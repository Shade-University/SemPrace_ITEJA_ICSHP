using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class WhileStatement : IStatement
    {
        public Condition Condition { get; } //Condition
        public List<IStatement> BodyStatements { get; } //Body

        public WhileStatement(Condition condition, List<IStatement> statements)
        {
            Condition = condition;
            BodyStatements = statements;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_WhileStatement(this);
        }
    }
}
