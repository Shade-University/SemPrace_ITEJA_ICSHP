namespace LanguageLogic.AST.Statements
{
    public abstract class Statement : IASTNode
    {
        public abstract object Visit(INodeVisitor visitor);
    }
}