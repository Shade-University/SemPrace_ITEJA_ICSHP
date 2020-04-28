﻿namespace LanguageLogic.AST.Statements
{
    public class FuncCallStatement : IStatement
    {
        public IStatement Function { get; } //Functions is also statemenets
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
