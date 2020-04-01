using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class IfStatement : IStatement
    {
        public Condition Condition { get; }
        public List<IStatement> BodyStatements { get; }

        public IfStatement(Condition condition, List<IStatement> statements)
        {
            Condition = condition;
            BodyStatements = statements;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_IfStatement(this);
        }
    }
}
