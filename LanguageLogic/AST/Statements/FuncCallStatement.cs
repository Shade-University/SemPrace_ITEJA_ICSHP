using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements
{
    public class FuncCallStatement : IStatement
    {
        public Token Function { get; } //TODO Maybe Enum with callable functions
        public FuncCallStatement(Token func)
        {
            Function = func;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_FuncCallStatement(this);
        }
    }
}
