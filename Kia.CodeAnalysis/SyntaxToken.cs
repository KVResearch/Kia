namespace Kia.CodeAnalysis;

public class SyntaxToken
{
    public SyntaxToken(TokenType type, int position, string text, object? value = null)
    {
        Type = type;
        Position = position;
        Text = text;
    }

    public TokenType Type { get; }
    public int Position { get; }
    public string Text { get; }
    public object? Value { get; }
}
