namespace Kia.CodeAnalysis.Syntax;

internal static class Precedence
{
    public static int GetUnaryExpressionPrecedence(this TokenType tokenType)
    {
        return tokenType switch
        {
            // Consider -1 * 3
            // Unary Oper always has higher precedence than binary oper
            TokenType.PlusToken or TokenType.MinusToken => 3,
            _ => 0
        };
    }
    
    public static int GetBinaryExpressionPrecedence(this TokenType tokenType)
    {
        return tokenType switch
        {
            TokenType.StarToken or TokenType.SlashToken => 2,
            TokenType.PlusToken or TokenType.MinusToken => 1,
            _ => 0
        };
    }
}
