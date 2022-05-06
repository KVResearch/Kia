using Kia.CodeAnalysis.Syntax;

namespace Kia.CodeAnalysis;
public class Parser
{
    private List<string> _diagnostics = new List<string>();
    public IEnumerable<string> Diagnostics => _diagnostics;
    private readonly SyntaxToken[] _tokens;
    private int _position = 0;
    public Parser(string text) : this(new Lexer(text), false)
    {
    }

    public Parser(Lexer lexer, bool resetLexer = true)
    {
        if (resetLexer)
        {
            lexer.ResetPosition();
            lexer.ClearDiagnostics();
        }
        var tokens = new List<SyntaxToken>();
        SyntaxToken token;
        do
        {
            token = lexer.NextSyntaxToken();
            tokens.Add(token);
        } while (token.TokenType != TokenType.EndOfFileToken);
        _tokens = tokens.ToArray();
        _diagnostics.AddRange(lexer.Diagnostics);
    }

    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;
        if (index >= _tokens.Length)
            return _tokens[_tokens.Length - 1];

        return _tokens[index];
    }

    private SyntaxToken Current => Peek(0);

    private SyntaxToken NextToken()
    {
        var current = Current;
        _position++;
        return current;
    }

    private SyntaxToken ExpectToken(TokenType type)
    {
        if (Current.TokenType == type)
            return NextToken();
        _diagnostics.Add($"[PARSER] Syntax error: Expected: {type}\n" +
                         $"                       Got     : {Current.TokenType}");
        return new SyntaxToken(type, Current.Position, null, null);
    }

    public SyntaxTree Parse()
    {
        var exp = ParseExpression();
        var eof = ExpectToken(TokenType.EndOfFileToken);
        return new(exp, eof, Diagnostics);
    }

    private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
    {
        ExpressionSyntax left;

        var unaryPrecedence = Current.TokenType.GetUnaryExpressionPrecedence();
        if (unaryPrecedence != 0 && unaryPrecedence >= parentPrecedence)
        {
            var oper = NextToken();
            var operand = ParseExpression(unaryPrecedence);
            left = new UnaryExpression(oper, operand);
        }
        else
        {
            left = ParsePrimaryExpression();
        }

        while (true)
        {
            var precedence = Current.TokenType.GetBinaryExpressionPrecedence();
            if (precedence == 0 || precedence <= parentPrecedence)
                break;

            var oper = NextToken();
            var right = ParseExpression(precedence);
            left = new BinaryExpression(left, oper, right);
        }
        return left;
    }
    // Parse () first!
    private ExpressionSyntax ParsePrimaryExpression()
    {
        if (Current.TokenType == TokenType.OpenParenthesisToken)
        {
            var left = NextToken();
            var expression = ParseExpression();
            var right = ExpectToken(TokenType.CloseParenthesisToken);
            return new ParenthesisedExpression(left, expression, right);
        }

        var numberToken = ExpectToken(TokenType.NumberToken);
        return new LiteralExpression(numberToken);
    }
}
