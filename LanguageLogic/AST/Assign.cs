using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class Assign : IASTNode
    {
        public IASTNode Left { get; }
        public IASTNode Right { get; }

        public Token Token { get; }

        public Assign(IASTNode left, IASTNode right, Token token)
        {
            Left = left;
            Right = right;
            Token = token;
        }
        public override object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Assign(this);
        }
    }
}
