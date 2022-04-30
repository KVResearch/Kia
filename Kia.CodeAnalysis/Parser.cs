using Kia.CodeAnalysis.Syntax;

namespace Kia.CodeAnalysis;
public class Parser
{
    private readonly SyntaxToken[] _tokens;
    private int _position = 0;
    public Parser(string text) : this(new Lexer(text))
    {
    }

    public Parser(Lexer lexer)
    {
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
        var exp = ParseTerm();
        var eof = ExpectToken(TokenType.EndOfFileToken);
        return new(exp, eof);
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
        return new NumberExpression(numberToken);
    }

    // Parse * and /
    private ExpressionSyntax ParseFactor()
    {
        var left = ParsePrimaryExpression();
        while (Current.Type is TokenType.StarToken
                            or TokenType.SlashToken)
        {
            var oper = NextToken();
            var right = ParsePrimaryExpression();
            left = new BinaryExpression(left, oper, right);
        }
        return left;
    }

    // Then Parse +, -
    private ExpressionSyntax ParseTerm()
    {
        var left = ParseFactor();

        while (Current.Type is TokenType.PlusToken
                            or TokenType.MinusToken)
        {
            var oper = NextToken();
            var right = ParseFactor();
            left = new BinaryExpression(left, oper, right);
        }

        return left;
    }

    private ExpressionSyntax ParseExpression()
    {
        return ParseTerm();
    }
}

