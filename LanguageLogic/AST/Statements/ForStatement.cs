namespace LanguageLogic.AST.Statements
{
    public class ForStatement : IStatement
    {
        public IExpression FromExpression { get; } //From
        public Block BodyBlock { get; } //Body
        public IExpression ToExpression { get; } //To

        public ForStatement(IExpression from, Block body, IExpression to)
        {
            FromExpression = from;
            ToExpression = to;
            BodyBlock = body;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_ForStatement(this);
        }
    }
}
