using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class FuncCallStatement : IStatement
    {
        public IStatement Function { get; }
        public FuncCallStatement(IStatement func)
        {
            Function = func;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_FuncCallStatement(this);
        }
    }
}
