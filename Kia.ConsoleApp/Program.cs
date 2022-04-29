// See https://aka.ms/new-console-template for more information
using Kia.CodeAnalysis;

while (true)
{
    Console.Write("> ");
    var s = Console.ReadLine();

    var lexer = new Lexer(s);

    while (true)
    {
        var token = lexer.NextSyntaxToken();
        if (token.Type == SyntaxTokenType.EndOfFileToken)
        {
            break;
        }
        Console.WriteLine($"{token.Type}: {token.Text}");
    }
}