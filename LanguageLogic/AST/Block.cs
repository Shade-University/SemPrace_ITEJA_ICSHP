using LanguageLogic.AST.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class Block : IASTNode
    {
        public List<Var> Declarations { get; set; } = new List<Var>(); //Variables
        public List<Statement> Childrens { get; set; } = new List<Statement>(); // Statements

        public Block(List<Var> declarations, List<Statement> childrens)
        {
            Declarations = declarations;
            Childrens = childrens;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Block(this);
        }
    }
}
