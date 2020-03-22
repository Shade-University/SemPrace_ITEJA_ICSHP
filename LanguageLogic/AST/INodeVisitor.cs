using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public interface INodeVisitor
    {
        object Visit_Assign(Assign node);
        object Visit_BinOp(BinOp node);
        object Visit_Block(Block node);
        object Visit_Num(Num node);
        object Visit_UnaryOp(UnaryOp node);
        object Visit_Var(Var node);
        object Visit_VarDeclaration(VarDeclaration node);


    }
}
