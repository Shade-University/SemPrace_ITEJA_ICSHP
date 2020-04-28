using LanguageLogic.Tokens;

namespace LanguageLogic.AST
{
    public class Condition : IASTNode
    {
        public IExpression Left { get; }
        public Token Token { get; } //Condition (<,>,<=,>=,==)
        public IExpression Right { get; }

        public Condition(IExpression left, Token token, IExpression right)
        {
            Left = left;
            Token = token;
            Right = right;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Condition(this);
        }
    }
}
