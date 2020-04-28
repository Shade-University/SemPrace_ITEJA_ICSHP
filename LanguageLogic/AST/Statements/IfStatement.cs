namespace LanguageLogic.AST.Statements
{
    public class IfStatement : IStatement
    {
        public Condition Condition { get; }
        public Block BodyBlock { get; } //Body

        public IfStatement(Condition condition, Block bodyBlock)
        {
            Condition = condition;
            BodyBlock = bodyBlock;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_IfStatement(this);
        }
    }
}
