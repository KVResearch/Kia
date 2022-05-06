namespace Kia.CodeAnalysis.Syntax;

internal class ParenthesisedExpression : ExpressionSyntax
{
    public override TokenType TokenType => TokenType.ParenthesisedExpression;

    public ParenthesisedExpression(SyntaxToken openParenthesis, ExpressionSyntax expression, SyntaxToken closeParenthesis)
    {
        OpenParenthesisToken = openParenthesis;
        Expression = expression;
        CloseParenthesisToken = closeParenthesis;
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
