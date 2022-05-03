namespace Kia.CodeAnalysis;

public enum TokenType
{
    NumberToken,           // 123
    WhitespaceToken,       // " "
    PlusToken,             // +
    MinusToken,            // -
    StarToken,             // *
    SlashToken,            // /
    OpenParenthesisToken,  // (
    CloseParenthesisToken, // )
    BadToken,
    EndOfFileToken,        // '\0'
    
    LiteralExpression,
    BinaryExpression,
    ParenthesisedExpression
}
