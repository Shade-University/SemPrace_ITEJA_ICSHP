using LanguageLogic.AST;
using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class BinOp : IASTNode
    {
        public IASTNode Left { get; }
        public IASTNode Right { get; }
        public Token Operation { get; } //Bin operace (+,-,*,/)
        public BinOp(IASTNode left, Token operation, IASTNode right)
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
