using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements.Functions
{
    public class ForwardStatement : IStatement
    {
        public IExpression Expression { get; }

        public ForwardStatement(IExpression expression)
        {
            Expression = expression;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_ForwardStatement(this);
        }
    }
}
