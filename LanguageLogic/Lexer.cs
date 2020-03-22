using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic
{
    public class Lexer
    {
        private Dictionary<string, Token> reservedKeyWords;

        private char currentChar;
        private int pos;
        private string text;

        public Lexer(string text)
        {
            this.text = text;
            pos = 0;
            currentChar = text[pos];

            reservedKeyWords = new Dictionary<string, Token>()
            {
                { "END", new Token() {TokenType = TokenType.END, Value = "END" } },
                { "VAR", new Token() {TokenType = TokenType.VAR, Value = "VAR" } },
                { "FUNC", new Token() {TokenType = TokenType.FUNC, Value = "FUNC" } },
                { "IF", new Token() {TokenType = TokenType.IF, Value = "IF" } },
                { "THEN", new Token() {TokenType = TokenType.THEN, Value = "THEN" } },
                { "WHILE", new Token() {TokenType = TokenType.WHILE, Value = "WHILE" } },
                { "DO", new Token() {TokenType = TokenType.DO, Value = "DO" } },
                { "FOR", new Token() {TokenType = TokenType.FOR, Value = "FOR" } },
                { "TO", new Token() {TokenType = TokenType.TO, Value = "TO" } },
            };
        }

        public Token GetNextToken()
        {
            while (currentChar != char.MinValue)
            {
                if (char.IsWhiteSpace(currentChar))
                {
                    SkipWhiteSpaces();
                    continue;
                }

                if (char.IsDigit(currentChar))
                {
                    return new Token() { TokenType = TokenType.NUMBER, Value = EatInt() };

                }

                if(char.IsLetter(currentChar))
                {
                    return EatId();
                }

                #region TwoChars
                if(currentChar == '<' && Peek() == '=')
                {
                    Advance();
                    Advance();
                    return new Token() { TokenType = TokenType.LESS_OR_EQUAL, Value = "<=" };
                }

                if (currentChar == '>' && Peek() == '=')
                {
                    Advance();
                    Advance();
                    return new Token() { TokenType = TokenType.MORE_OR_EQUAL, Value = ">=" };
                }

                if (currentChar == '=' && Peek() == '=')
                {
                    Advance();
                    Advance();
                    return new Token() { TokenType = TokenType.EQUALS, Value = "==" };
                }
                #endregion

                #region OneChar

                if (currentChar == '{')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.LBRACKET, Value = "{" };
                }

                if (currentChar == '}')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.RBRACKET, Value = "}" };
                }

                if (currentChar == '(')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.LPARENT, Value = "(" };
                }

                if (currentChar == ')')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.RPARENT, Value = ")" };
                }

                if (currentChar == '.')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.DOT, Value = "." };
                }

                if (currentChar == ',')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.COMA, Value = "," };
                }

                if (currentChar == ';')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.SEMICOLON, Value = ";" };
                }

                if (currentChar == '=')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.ASSIGN, Value = "=" };
                }

                if (currentChar == '<')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.LESS, Value = "<" };
                }

                if (currentChar == '>')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.MORE, Value = ">" };
                }

                if (currentChar == '+')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.PLUS, Value = "+" };
                }

                if (currentChar == '-')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.MINUS, Value = "-" };
                }

                if (currentChar == '*')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.MUL, Value = "*" };
                }

                if (currentChar == '/')
                {
                    Advance();
                    return new Token() { TokenType = TokenType.DIV, Value = "/" };
                }

                #endregion

                throw new Exception("Unknown token"); //Unknown token
            }

            return new Token() { TokenType = TokenType.EOF, Value = "NONE" }; //end of file
        }

        private void Advance()
        {
            pos += 1;
            if (pos > text.Length - 1)
                currentChar = char.MinValue;
            else
                currentChar = text[pos];
        }

        private char Peek()
        {
            int peek_position = pos + 1;
            if (peek_position > text.Length - 1)
                return char.MinValue;
            else
                return text[peek_position];
        }

        private Token EatId()
        {
            string result = "";
            while (currentChar != char.MinValue && char.IsLetter(currentChar))
            {
                result += currentChar;
                Advance();
            }

            if (reservedKeyWords.TryGetValue(result.ToUpper(), out Token keyword))
            {
                return keyword;
            }

            return new Token() { TokenType = TokenType.IDENT, Value = result };
        }

        private string EatInt()
        {
            string result = "";
            while (currentChar != char.MinValue && char.IsDigit(currentChar))
            {
                result += currentChar;
                Advance();
            }
            return result;
            //TODO Double numbers
        }

        private void SkipWhiteSpaces()
        {
            while (currentChar != char.MinValue && char.IsWhiteSpace(currentChar))
                Advance();
        }

        //TODO Skip comments
    }
}
