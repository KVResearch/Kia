namespace Kia.CodeAnalysis.Syntax;

public class SyntaxTree
{
    public IEnumerable<string> Diagnostics;
    public SyntaxTree(ExpressionSyntax root, SyntaxToken endOfFileToken, IEnumerable<string> diagnostics)
    {
        Root = root;
        EndOfFileToken = endOfFileToken;
        Diagnostics = diagnostics;
    }
    public ExpressionSyntax Root { get; }
    public SyntaxToken EndOfFileToken { get; }

    public static SyntaxTree Parse(string text)
    {
        var parser = new Parser(text);
        return parser.Parse();
    }

    public static void Print(SyntaxNode node, string indent = "", bool isLast = true)
    {
        var marker = isLast ? "└──" : "├──";

        Console.Write(indent);
        Console.Write(marker);

        ConsoleColor colour = ConsoleColor.White;

        var type = node.TokenType.ToString();
        if (type.EndsWith("Token"))
            colour = ConsoleColor.DarkCyan;

        if (node.TokenType == TokenType.NumberToken)
            colour = ConsoleColor.DarkGreen;

        if (node.TokenType == TokenType.BadToken)
            colour = ConsoleColor.DarkRed;

        if (type.EndsWith("Expression"))
            colour = ConsoleColor.DarkYellow;

        PrintColourly(node.TokenType, colour);

        if (node is SyntaxToken t)
        {
            var value = t.Value == null
                      ? @"-> NULL"
                      : $"-> {t.Value}";
            Console.Write($" {t.Text} {value}");
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

    public void PrintDiaognostics()
    {
        foreach (var d in Diagnostics)
            PrintColourly(d, ConsoleColor.DarkRed, true);
    }

    private static void PrintColourly(object? text, ConsoleColor colour, bool newLine = false)
    {
        Console.ForegroundColor = colour;
        if (newLine)
            Console.WriteLine(text);
        else
            Console.Write(text);
        Console.ResetColor();
    }
}
