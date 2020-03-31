using LanguageLogic.AST.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public interface INodeVisitor //Visitor pattern
    {
        object Visit_Assign(AssignStatement node);
        object Visit_BinOp(BinOp node);
        object Visit_FuncCallStatement(FuncCallStatement funcCallStatement);
        object Visit_Block(Block node);
        object Visit_Num(Num node);
        object Visit_UnaryOp(UnaryOp node);
        object Visit_Var(Var node);
        object Visit_VarDeclaration(VarDeclaration node);
        object Visit_WhileStatement(WhileStatement whileStatement);
        object Visit_IfStatement(IfStatement ifStatement);
        object Visit_Condition(Condition node);
        object Visit_ForStatement(ForStatement node);
    }
}
