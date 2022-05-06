namespace Kia.CodeAnalysis.Syntax;

internal class LiteralExpression : ExpressionSyntax
{
    public override TokenType TokenType => TokenType.LiteralExpression;

    public LiteralExpression(SyntaxToken token)
    {
        Token = token;
    }

    public SyntaxToken Token { get; }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Token;
    }
}