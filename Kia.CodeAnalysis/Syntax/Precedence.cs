namespace Kia.CodeAnalysis.Syntax;

internal static class Precedence
{
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
