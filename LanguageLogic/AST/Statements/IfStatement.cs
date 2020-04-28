using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class IfStatement : IStatement
    {
        public Condition Condition { get; }
        public List<IStatement> BodyStatements { get; } //TODO Tady by měl být block!!!! A všude i ve while atd

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
