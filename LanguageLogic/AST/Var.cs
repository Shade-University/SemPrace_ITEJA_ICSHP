using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public enum VarType
    {
        STRING,
        DOUBLE,
        NONE
    }

    public class Var : IExpression
    {
        public string Identifier { get; } //Variable name
        public VarType Type { get; set; }
        public Token Token { get; }

        public Var(Token token)
        {
            Identifier = token.Value;
            Token = token;
            Type = VarType.NONE;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Var(this);
        }
    }
}
