using LanguageLogic.AST;
using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class Num :IASTNode //Node representing number
    {
        public double Value { get; } //Value of that number
        public Token Token { get; }

        public Num(Token token)
        {
            Value = double.Parse(token.Value);
            Token = token;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Num(this);
        }

    }
}
