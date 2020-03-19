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
        public override double Visit()
        {
            if(Token.TokenType == TokenType.PLUS)
            {
                return +Node.Visit();
            }
            else if(Token.TokenType == TokenType.MINUS)
            {
                return -Node.Visit();
            }

            throw new Exception("Unknown UnaryOP TokenType");
        }
    }
}
