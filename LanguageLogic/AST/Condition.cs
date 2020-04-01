using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class Condition : IASTNode
    {
        public IExpression Left { get;} //Variable or some expression (number)
        public Token Token { get;} //Condition (<,>,<=,>=,==)
        public IExpression Right { get;} //Variable or some expression (number)

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
