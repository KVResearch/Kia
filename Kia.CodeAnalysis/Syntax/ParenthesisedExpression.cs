namespace Kia.CodeAnalysis.Syntax;

internal class ParenthesisedExpression : ExpressionSyntax
{
    public override TokenType Type => TokenType.ParenthesisedExpression;

    public ParenthesisedExpression(SyntaxToken openParenthesisToken, ExpressionSyntax expression, SyntaxToken closeParenthesisToken)
    {
        OpenParenthesisToken = openParenthesisToken;
        Expression = expression;
        CloseParenthesisToken = closeParenthesisToken;
    }
    public SyntaxToken OpenParenthesisToken { get; }
    
    public ExpressionSyntax Expression { get; }
    
    public SyntaxToken CloseParenthesisToken { get; }    
    

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return OpenParenthesisToken;
        yield return Expression;
        yield return CloseParenthesisToken;
    }
}
