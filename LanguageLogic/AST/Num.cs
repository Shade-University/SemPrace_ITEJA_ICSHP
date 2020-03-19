using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic
{
    public class Num :IASTNode
    {
        public double Value { get; }
        public Token Token { get; }

        public Num(Token token)
        {
            Value = double.Parse(token.Value);
            Token = token;
        }

        public override double Visit()
        {
            return Value;
        }

    }
}
