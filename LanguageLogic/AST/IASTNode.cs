using LanguageLogic.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public interface IASTNode //Interface representing Node in AST
    {
        public object Visit(INodeVisitor visitor);
    }
}
