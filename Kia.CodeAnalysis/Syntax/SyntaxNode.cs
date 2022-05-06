namespace Kia.CodeAnalysis.Syntax;

public abstract class SyntaxNode
{
    public abstract TokenType TokenType { get; }
    public abstract IEnumerable<SyntaxNode> GetChildren();
}