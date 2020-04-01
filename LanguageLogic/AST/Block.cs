using LanguageLogic.AST.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class Block : IASTNode
    {
        public List<Var> Declarations { get; } = new List<Var>(); //Variables
        public List<IStatement> BodyStatements { get; } = new List<IStatement>(); // Statements

        public Block(List<Var> declarations, List<IStatement> childrens)
        {
            Declarations = declarations;
            BodyStatements = childrens;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Block(this);
        }
    }
}
