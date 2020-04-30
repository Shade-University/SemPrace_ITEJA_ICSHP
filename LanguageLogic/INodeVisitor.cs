using LanguageLogic.AST;
using LanguageLogic.AST.Statements;
using LanguageLogic.AST.Statements.Functions;

namespace LanguageLogic
{
    public interface INodeVisitor //Visitor pattern
    {
        object Visit_Assign(AssignStatement node);
        object Visit_BinOp(BinOp node);
        object Visit_FuncCallStatement(FuncCallStatement funcCallStatement);
        object Visit_AngleStatement(AngleStatement angleStatement);
        object Visit_BackwardStatement(BackwardStatement backwardStatement);
        object Visit_ForwardStatement(ForwardStatement forwardStatement);
        object Visit_Block(Block node);
        object Visit_Num(Num node);
        object Visit_WriteStatement(WriteStatement writeStatement);
        object Visit_UnaryOp(UnaryOp node);
        object Visit_Var(Var node);
        object Visit_VarDeclaration(VarDeclaration node);
        object Visit_WhileStatement(WhileStatement whileStatement);
        object Visit_PenStatement(PenStatement penStatement);
        object Visit_IfStatement(IfStatement ifStatement);
        object Visit_Condition(Condition node);
        object Visit_ForStatement(ForStatement node);
        object Visit_StringText(StringText stringText); //Returning object for multiple purposes like gui Visitor. Null for returning nothing
    }
}
