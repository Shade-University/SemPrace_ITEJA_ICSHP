using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class AssignStatement : IStatement
    {
        public string Identificator { get; } // Variable
        public IExpression Expression { get; } //EXpression to assign to variable
        public Token Token { get; } // :=

        public AssignStatement(string ident, IExpression expression, Token token)
        {
            Identificator = ident;
            Expression = expression;
            Token = token;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Assign(this);
        }
    }
}
