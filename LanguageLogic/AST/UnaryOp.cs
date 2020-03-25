using LanguageLogic.AST;
using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class UnaryOp : IASTNode
    {
        public IASTNode Node { get; } //Number or expression
        public Token Token { get; } //Unary operator (+,-)

        public UnaryOp(Token token, IASTNode node)
        {
            Node = node;
            Token = token;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_UnaryOp(this);
        }
    }
}
