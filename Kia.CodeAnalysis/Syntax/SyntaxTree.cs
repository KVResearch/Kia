namespace Kia.CodeAnalysis.Syntax;

public class SyntaxTree
{
    public SyntaxTree(ExpressionSyntax root, SyntaxToken endOfFileToken)
    {
        Root = root;
        EndOfFileToken = endOfFileToken;
    }
    public ExpressionSyntax Root { get; }
    public SyntaxToken EndOfFileToken { get; }

    public static SyntaxTree Parse(string text)
    {
        var parser = new Parser(text);
        return null;
    }

    public static void Print(SyntaxNode node, string indent = "", bool isLast = true)
    {
        var marker = isLast ? "└──" : "├──";

        Console.Write(indent);
        Console.Write(marker);

        ConsoleColor colour = ConsoleColor.White;

        var type = node.Type.ToString();
        if (type.EndsWith("Token"))
            colour = ConsoleColor.DarkCyan;

        if (node.Type == TokenType.NumberToken)
            colour = ConsoleColor.DarkGreen;

        if (node.Type == TokenType.BadToken)
            colour = ConsoleColor.DarkRed;

        if (type.EndsWith("Expression"))
            colour = ConsoleColor.DarkYellow;

        PrintColourly(node.Type, colour);

        if (node is SyntaxToken t)
        {
            Console.Write(" ");
            Console.Write(t.Text);
        }

        Console.WriteLine();

        indent += isLast ? "    " : "│   ";

        var lastChild = node.GetChildren().LastOrDefault();

        foreach (var child in node.GetChildren())
            Print(child, indent, child == lastChild);
    }

    public void Print()
    {
        Print(Root);
    }

    private static void PrintColourly(object? text, ConsoleColor colour)
    {
        Console.ForegroundColor = colour;
        Console.Write(text);
        Console.ResetColor();
    }
}
