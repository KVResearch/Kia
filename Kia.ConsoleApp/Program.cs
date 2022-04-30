using Kia.CodeAnalysis;

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
    var parser = new Parser(lexer);
    var e = parser.Parse();
    e.Print();
}
