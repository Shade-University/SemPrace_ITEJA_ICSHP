using LanguageLogic.Tokens;

namespace LanguageLogic.AST
{
    public class UnaryOp : IExpression
    {
        public IExpression Expression { get; } //Number or expression
        public Token Token { get; } //Unary operator (+,-)

        public UnaryOp(Token token, IExpression node)
        {
            Expression = node;
            Token = token;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_UnaryOp(this);
        }
    }
}
