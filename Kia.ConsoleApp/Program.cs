using Kia.CodeAnalysis;
using Kia.CodeAnalysis.Syntax;

while (true)
{
    Console.Write("> ");
    var s = Console.ReadLine();

    var lexer = new Lexer(s);


    Console.WriteLine("######### LEXER #########");
    while (true)
    {
        var token = lexer.NextSyntaxToken();
        Console.WriteLine($"{token.Type}: {token.Text}");
        if (token.Type == TokenType.EndOfFileToken)
        {
            break;
        }
    }

    Console.WriteLine("######## PARSER #########");
    var parser = new Parser(s);
    var e = parser.Parse();
    PrettyPrint(e.Root);

}

static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
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
        PrettyPrint(child, indent, child == lastChild);
}

static void PrintColourly(object? text, ConsoleColor colour)
{
    Console.ForegroundColor = colour;
    Console.Write(text);
    Console.ResetColor();
}