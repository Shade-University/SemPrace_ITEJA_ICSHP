using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class ForStatement : IStatement
    {
        public IExpression FromExpression { get; } //From
        public List<IStatement> BodyStatements { get; }
        public IExpression ToExpression { get; } //To

        public ForStatement(IExpression from, List<IStatement> body, IExpression to)
        {
            FromExpression = from;
            ToExpression = to;
            BodyStatements = body;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_ForStatement(this);
        }
    }
}
