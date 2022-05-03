using Kia.CodeAnalysis.Syntax;

namespace Kia.CodeAnalysis;
public class Parser
{
    private readonly SyntaxToken[] _tokens;
    private int _position = 0;
    public Parser(string text) : this(new Lexer(text), false)
    {
    }

    public Parser(Lexer lexer, bool resetLexer = true)
    {
        if (resetLexer)
            lexer.ResetPosition();
        var tokens = new List<SyntaxToken>();
        SyntaxToken token;
        do
        {
            token = lexer.NextSyntaxToken();
            tokens.Add(token);
        } while (token.Type != TokenType.EndOfFileToken);
        _tokens = tokens.ToArray();
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
        if (Current.Type == type)
            return NextToken();
        return new SyntaxToken(type, Current.Position, null, null);
    }

    public SyntaxTree Parse()
    {
        var exp = ParseExpression();
        var eof = ExpectToken(TokenType.EndOfFileToken);
        return new(exp, eof);
    }

    private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
    {
        var left = ParsePrimaryExpression();

        while (true)
        {
            var precedence = Current.Type.GetBinaryExpressionPrecedence();
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
        if (Current.Type == TokenType.OpenParenthesisToken)
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
