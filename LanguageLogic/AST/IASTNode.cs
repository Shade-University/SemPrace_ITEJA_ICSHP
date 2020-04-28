namespace LanguageLogic.AST
{
    public interface IASTNode //Interface representing Node in AST
    {
        public object Visit(INodeVisitor visitor);
    }
}
