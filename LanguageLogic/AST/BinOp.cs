﻿using LanguageLogic.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic
{
    public class BinOp : IASTNode
    {

        public IASTNode Left { get; }
        public IASTNode Right { get; }

        public Token Operation { get; }
        public BinOp(IASTNode left, Token operation, IASTNode right)
        {
            Left = left;
            Operation = operation;
            Right = right;
        }

        public override object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_BinOp(this);
        }

    }
}
