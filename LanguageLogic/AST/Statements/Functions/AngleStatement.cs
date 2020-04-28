using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements.Functions
{
    public class AngleStatement : IStatement
    {
        public IExpression Angle { get; }

        public AngleStatement(IExpression expression)
        {
            Angle = expression;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_AngleStatement(this);
        }
    }
}
