using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class AssignStatement : Statement
    {
        public Var Left { get; } // Variable
        public IASTNode Right { get; } //EXpression to assign to variable

        public Token Token { get; } // :=

        public AssignStatement(Var left, IASTNode right, Token token)
        {
            Left = left;
            Right = right;
            Token = token;
        }
        public override object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Assign(this);
        }
    }
}
