using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class Condition
    {
        public IASTNode Left { get; set; } //Variable or some expression (number)
        public Token Token { get; set; } //Condition (<,>,<=,>=,==)
        public IASTNode Right { get; set; } //Variable or some expression (number)

        public Condition(IASTNode left, Token token, IASTNode right)
        {
            Left = left;
            Token = token;
            Right = right;
        }
    }
}
