using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class VarDeclaration : IASTNode
    {
        public Var Variable { get; }

        public VarDeclaration(Var node)
        {
            Variable = node;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_VarDeclaration(this);
        }
    }
}
