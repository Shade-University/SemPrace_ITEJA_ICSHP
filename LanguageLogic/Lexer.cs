using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;

namespace LanguageLogic
{
    public class Lexer
    {
        public string Text { get; }

        private Dictionary<string, Token> reservedKeyWords;
        private char currentChar;
        private int pos;

        public Lexer(string text)
        {
            Text = text;
            pos = 0;
            currentChar = text[pos];

            reservedKeyWords = new Dictionary<string, Token>()
            {
                { "END", new Token() {TokenType = TokenType.END, Value = "END" } },
                { "var", new Token() {TokenType = TokenType.VAR, Value = "var" } },
                { "func", new Token() {TokenType = TokenType.FUNC, Value = "func" } },
                { "if", new Token() {TokenType = TokenType.IF, Value = "if" } },
                { "then", new Token() {TokenType = TokenType.THEN, Value = "then" } },
                { "while", new Token() {TokenType = TokenType.WHILE, Value = "while" } },
                { "do", new Token() {TokenType = TokenType.DO, Value = "do" } },
                { "for", new Token() {TokenType = TokenType.FOR, Value = "for" } },
                { "to", new Token() {TokenType = TokenType.TO, Value = "to" } },

                { "write", new Token() {TokenType = TokenType.WRITE, Value = "write" } },
                { "pen", new Token() {TokenType = TokenType.PEN, Value = "pen" } },
                { "angle", new Token() {TokenType = TokenType.ANGLE, Value = "angle" } },
                { "backward", new Token() {TokenType = TokenType.BACKWARD, Value = "backward" } },
                { "forward", new Token() {TokenType = TokenType.FORWARD, Value = "forward" } },

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

                if (char.IsLetter(currentChar))
                {
                    return EatId();
                }

                #region TwoChars
                if (currentChar == '!' && Peek() == '=')
                {
                    Advance();
                    Advance();
                    return new Token() { TokenType = TokenType.NOT_EQUAL, Value = "!=" };
                }
                if (currentChar == '<' && Peek() == '=')
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

                if (currentChar == '"')
                {
                    return EatString();
                }

                #endregion

                throw new Exception("Unknown token"); //Unknown token
            }

            return new Token() { TokenType = TokenType.EOF, Value = "NONE" }; //end of file
        }

        private void Advance()
        {
            pos += 1;
            if (pos > Text.Length - 1)
            {
                currentChar = char.MinValue;
            }
            else
            {
                currentChar = Text[pos];
            }
        }

        private char Peek()
        {
            int peek_position = pos + 1;
            if (peek_position > Text.Length - 1)
            {
                return char.MinValue;
            }
            else
            {
                return Text[peek_position];
            }
        }

        private Token EatId()
        {
            string result = "";
            while (currentChar != char.MinValue && char.IsLetter(currentChar))
            {
                result += currentChar;
                Advance();
            }

            if (reservedKeyWords.TryGetValue(result, out Token keyword))
            {
                return keyword;
            }

            return new Token() { TokenType = TokenType.IDENT, Value = result };
        }

        private Token EatString()
        {
            string result = "";
            Advance(); //Skip "
            while (currentChar != char.MinValue && !currentChar.Equals('"'))
            {
                result += currentChar;
                Advance();
            }
            Advance(); //Skip "

            return new Token() { TokenType = TokenType.TEXT, Value = result };
        }

        private string EatInt()
        {
            string result = "";
            while (currentChar != char.MinValue && char.IsDigit(currentChar))
            {
                result += currentChar;
                Advance();
            }

            if (currentChar.Equals(','))
            {
                result += currentChar;
                Advance();

                while (currentChar != char.MinValue && char.IsDigit(currentChar))
                {
                    result += currentChar;
                    Advance();
                }
            }
            return result;
        }

        private void SkipWhiteSpaces()
        {
            while (currentChar != char.MinValue && char.IsWhiteSpace(currentChar))
            {
                Advance();
            }
        }
    }
}
