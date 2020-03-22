using LanguageLogic.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic
{
    public class UnaryOp : IASTNode
    {

        public IASTNode Node { get; }
        public Token Token { get; }

        public UnaryOp(Token token, IASTNode node)
        {
            Node = node;
            Token = token;
        }
        public override object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_UnaryOp(this);
        }
    }
}
