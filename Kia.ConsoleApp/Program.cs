using Kia.CodeAnalysis;

while (true)
{
    Console.Write("> ");
    var s = Console.ReadLine()!;

    var lexer = new Lexer(s);


    Console.WriteLine("======= LEXER =======");
    while (true)
    {
        var token = lexer.NextSyntaxToken();
        Console.WriteLine($"{token.TokenType}: {token.Text}");
        if (token.TokenType == TokenType.EndOfFileToken)
        {
            break;
        }
    }

    Console.WriteLine("======= PARSER =======");
    var parser = new Parser(lexer);
    var e = parser.Parse();
    e.Print();
    Console.WriteLine("======= EVALUE =======");
    try
    {
        var evaluator = new Evaluator(e.Root);
        Console.WriteLine(evaluator.Evaluate());
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    Console.WriteLine("======= DIAGN =======");
    e.PrintDiaognostics();

}
