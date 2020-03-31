using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class ForStatement : Statement
    {

        public IASTNode Left { get; set; } //From

        List<Statement> Body { get; set; }
        public IASTNode Right { get; set; } //To

        public ForStatement(IASTNode left, List<Statement> body, IASTNode right)
        {
            Left = left;
            Right = right;
            Body = Body;
        }
        public override object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_ForStatement(this);
        }
    }
}
