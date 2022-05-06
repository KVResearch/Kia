using Kia.CodeAnalysis.Syntax;

namespace Kia.CodeAnalysis;

public class Lexer
{
    private List<string> _diagnostics = new List<string>();
    private readonly string _text;
    private int _position;

    public IEnumerable<string> Diagnostics => _diagnostics;

    public Lexer(string text)
    {
        _text = text;
        _position = 0;
    }

    public void ResetPosition()
    {
        _position = 0;
    }

    public void ClearDiagnostics()
    {
        _diagnostics.Clear();
    }

    public char CurrentChar
        => _position >= _text.Length
            ? '\0'
            : _text[_position];

    public void Next() => _position++;

    public SyntaxToken NextSyntaxToken()
    {
        if (_position >= _text.Length || _text[_position] == '\0')
            return new SyntaxToken(TokenType.EndOfFileToken, _position, "\0");

        if (char.IsDigit(CurrentChar))
        {
            var begin = _position;
            while (char.IsDigit(CurrentChar))
                Next();
            var tokenText = _text.Substring(begin, _position - begin);

            if (int.TryParse(tokenText, out var value))
            {
                return new SyntaxToken(
                               TokenType.NumberToken,
                               begin,
                               tokenText,
                               value
                           );
            }
            else
            {
                _diagnostics.Add($"[LEXER] int32 casting failed: {tokenText}");
            }
        }

        if (char.IsWhiteSpace(CurrentChar))
        {
            var begin = _position;
            while (char.IsWhiteSpace(CurrentChar))
                Next();
            return new SyntaxToken(
                TokenType.WhitespaceToken,
                begin,
                _text.Substring(begin, _position - begin)
            );
        }

        var token = CurrentChar switch
        {
            '+' => new SyntaxToken(TokenType.PlusToken, _position++, "+"),
            '-' => new SyntaxToken(TokenType.MinusToken, _position++, "-"),
            '*' => new SyntaxToken(TokenType.StarToken, _position++, "*"),
            '/' => new SyntaxToken(TokenType.SlashToken, _position++, "/"),
            '(' => new SyntaxToken(TokenType.OpenParenthesisToken, _position++, "("),
            ')' => new SyntaxToken(TokenType.CloseParenthesisToken, _position++, ")"),
            _ => new SyntaxToken(TokenType.BadToken, text: CurrentChar.ToString(), position: _position++),
        };

        if (token.TokenType == TokenType.BadToken)
        {
            _diagnostics.Add($"[LEXER] Bad token: {token.Text}");
        }
        return token;
    }
}