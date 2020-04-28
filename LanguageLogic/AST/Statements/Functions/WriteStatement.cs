namespace LanguageLogic.AST.Statements.Functions
{
    public class WriteStatement : IStatement
    {
        public IExpression Expression { get; }

        public WriteStatement(IExpression expression)
        {
            Expression = expression;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_WriteStatement(this);
        }
    }
}
