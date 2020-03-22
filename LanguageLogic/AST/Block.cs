using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class Block : IASTNode
    {
        public List<IASTNode> Declarations { get; set; }
        public List<IASTNode> Childrens { get; set; }


        public Block()
        {
            Childrens = new List<IASTNode>();
            Declarations = new List<IASTNode>();
        }

        public Block(List<IASTNode> declarations, List<IASTNode> childrens)
        {
            Declarations = declarations;
            Childrens = childrens;
        }

        public override object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Block(this);
        }
    }
}
