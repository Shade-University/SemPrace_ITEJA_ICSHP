namespace LanguageLogic.AST.Statements.Functions
{
    public class BackwardStatement : IStatement
    {
        public IExpression Expression { get; }

        public BackwardStatement(IExpression expression)
        {
            Expression = expression;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_BackwardStatement(this);
        }
    }
}
