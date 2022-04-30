namespace Kia.CodeAnalysis.Syntax;

public class SyntaxToken : SyntaxNode
{
    public SyntaxToken(TokenType type, int position, string text, object? value = null)
    {
        Type = type;
        Position = position;
        Text = text;
    }

    public override TokenType Type { get; }

    public int Position { get; }
    public string Text { get; }
    public object? Value { get; }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        return Enumerable.Empty<SyntaxNode>();
    }
}
