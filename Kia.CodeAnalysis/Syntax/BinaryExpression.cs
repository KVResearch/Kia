namespace Kia.CodeAnalysis.Syntax;

internal class BinaryExpression : ExpressionSyntax
{
    public override TokenType Type => TokenType.BinaryExpression;
    public ExpressionSyntax Left { get; }
    public SyntaxToken Operator { get; }
    public ExpressionSyntax Right { get; }

    public BinaryExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
    {
        Left = left;
        Operator = operatorToken;
        Right = right;
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Left;
        yield return Operator;
        yield return Right;
    }
}