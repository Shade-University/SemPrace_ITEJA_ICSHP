using LanguageLogic.Tokens;

namespace LanguageLogic.AST
{
    public class BinOp : IExpression
    {
        public IExpression Left { get; }
        public IExpression Right { get; }
        public Token Operation { get; } //Bin operace (+,-,*,/)
        public BinOp(IExpression left, Token operation, IExpression right)
        {
            Left = left;
            Operation = operation;
            Right = right;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_BinOp(this);
        }

    }
}
