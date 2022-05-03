namespace Kia.CodeAnalysis.Syntax;

public class UnaryExpression : ExpressionSyntax
{
    public override TokenType Type => TokenType.UnaryExpression;

    public SyntaxToken Operator { get; }
    public ExpressionSyntax Operand { get; }

    public UnaryExpression(SyntaxToken oper, ExpressionSyntax operand)
    {
        Operator = oper;
        Operand = operand;
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Operator;
        yield return Operand;
    }
}