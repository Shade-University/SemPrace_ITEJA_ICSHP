using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic
{
    public class Lexer
    {
        private char currentChar;
        private int pos;
        private string text;

        public Lexer(string text)
        {
            this.text = text;
            pos = 0;
            currentChar = text[pos];
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

        private string EatInt()
        {
            string result = "";
            while (currentChar != char.MinValue && char.IsDigit(currentChar))
            {
                result += currentChar;
                Advance();
            }
            return result;
        }

        private void SkipWhiteSpaces()
        {
            while (currentChar != char.MinValue && char.IsWhiteSpace(currentChar))
                Advance();
        }
    }
}
