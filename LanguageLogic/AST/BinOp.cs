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

        public override double Visit()
        {
            if (Operation.TokenType == TokenType.PLUS)
            {
                return Left.Visit() + Right.Visit();
            }
            else if (Operation.TokenType == TokenType.MINUS)
            {
                return Left.Visit() - Right.Visit();
            }
            else if (Operation.TokenType == TokenType.MUL)
            {
                return Left.Visit() * Right.Visit();
            }
            else if (Operation.TokenType == TokenType.DIV)
            {
                return Left.Visit() / Right.Visit();
            }

            throw new Exception("Unknown BinOP TokenType");
        }

    }
}
