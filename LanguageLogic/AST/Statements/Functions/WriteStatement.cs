using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements.Functions
{
    public class WriteStatement : IStatement
    {
        public IExpression Expression { get; } //Variable or number

        public WriteStatement(IExpression expression)
        {
            this.Expression = expression;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_WriteStatement(this);
        }
    }
}
