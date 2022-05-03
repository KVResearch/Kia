using Kia.CodeAnalysis.Syntax;

namespace Kia.CodeAnalysis;
public class Evaluator
{
    public ExpressionSyntax Root { get; set; }

    public Evaluator(ExpressionSyntax root)
    {
        Root = root;
    }

    public int Evaluate()
    {
        return EvaluateExpression(Root);
    }

    public static int EvaluateExpression(ExpressionSyntax rxp)
    {
        if (rxp is NumberExpression n)
            return int.Parse(n.Token.Text);


        if (rxp is ParenthesisedExpression p)
            return EvaluateExpression(p.Expression);

        if (rxp is BinaryExpression b)
        {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            return b.Operator.Type switch
            {
                TokenType.PlusToken => left + right,
                TokenType.MinusToken => left - right,
                TokenType.StarToken => left * right,
                TokenType.SlashToken => left / right,
                _ => throw new Exception($"Unknown operator: {b.Operator.Type}")
            };
        }


        throw new Exception($"Unexpected type: {rxp.Type}");
    }
}
