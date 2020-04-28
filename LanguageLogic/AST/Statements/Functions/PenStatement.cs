using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST.Statements.Functions
{
    public enum PenStatus
    {
        UP, DOWN
    }
    public class PenStatement : IStatement
    {
        public PenStatus PenStatus { get; set; } = PenStatus.UP;

        public PenStatement(PenStatus penStatus)
        {
            PenStatus = penStatus;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_PenStatement(this);
        }
    }
}
