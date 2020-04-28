namespace LanguageLogic.AST.Statements
{
    public class WhileStatement : IStatement
    {
        public Condition Condition { get; } //Condition
        public Block BodyBlock { get; } //Body

        public WhileStatement(Condition condition, Block block)
        {
            Condition = condition;
            BodyBlock = block;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_WhileStatement(this);
        }
    }
}
