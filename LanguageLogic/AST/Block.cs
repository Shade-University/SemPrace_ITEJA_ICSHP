using LanguageLogic.AST.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class Block : IASTNode
    {
        public List<VarDeclaration> Declarations { get; } = new List<VarDeclaration>(); //Empty variables
        public List<IStatement> BodyStatements { get; } = new List<IStatement>(); // Statements

        public Block(List<VarDeclaration> declarations, List<IStatement> childrens)
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
