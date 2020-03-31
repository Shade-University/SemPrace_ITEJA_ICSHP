using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class WhileStatement : Statement
    {
        public Condition Condition { get; set; } //Condition
        public List<Statement> BodyStatements { get; set; } //Body

        public WhileStatement(Condition condition, List<Statement> statements)
        {
            Condition = condition;
            BodyStatements = statements;
        }
        public override object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_WhileStatement(this);
        }
    }
}
