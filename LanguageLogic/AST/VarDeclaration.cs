using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class VarDeclaration : IASTNode
    {
        Var Node { get; set; }
        //TODO Type

        public VarDeclaration(Var node)
        {
            Node = node;
        }

        public override object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_VarDeclaration(this);
        }
    }
}
