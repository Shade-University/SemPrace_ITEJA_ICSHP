using LanguageLogic.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic
{
    public abstract class IASTNode
    {
        public abstract object Visit(INodeVisitor visitor);
    }
}
