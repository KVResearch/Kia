namespace Kia.CodeAnalysis.Syntax;

internal class NumberExpression : ExpressionSyntax
{
    public override TokenType Type => TokenType.NumberExpression;

    public NumberExpression(SyntaxToken token)
    {
        Token = token;
    }

    public SyntaxToken Token { get; }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Token;
    }
}