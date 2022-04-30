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


        var colour = node.Type switch
        {
            TokenType.NumberToken => ConsoleColor.DarkGreen,
            TokenType.MinusToken or
            TokenType.PlusToken or
            TokenType.StarToken or
            TokenType.SlashToken => ConsoleColor.DarkCyan,
            TokenType.BadToken => ConsoleColor.Red,
            _ => ConsoleColor.White,
        };
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
