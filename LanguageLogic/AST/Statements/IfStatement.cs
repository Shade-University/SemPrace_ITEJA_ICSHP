using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class IfStatement : Statement
    {
        public Condition Condition { get; set; }
        public List<Statement> BodyStatements { get; set; }

        public IfStatement(Condition condition, List<Statement> statements)
        {
            Condition = condition;
            BodyStatements = statements;
        }

        public override object Visit(INodeVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
