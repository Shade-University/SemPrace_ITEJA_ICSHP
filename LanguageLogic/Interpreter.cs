using LanguageLogic.AST;
using LanguageLogic.AST.Statements;
using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic
{
    public class Interpreter : INodeVisitor
    {
        private Parser parser;

        private Dictionary<string, int> variables;
        public Interpreter(Parser parser)
        {
            this.parser = parser;
            variables = new Dictionary<string, int>();
        }


        public void Interpret()
        {
            //TODO
        }

        public object Visit_Assign(AssignStatement node)
        {
            variables.Add(
                node.Identificator,
                (int)node.Expression.Visit(this)
              );

            return null;
        }

        public object Visit_BinOp(BinOp node)
        {
            if (node.Operation.TokenType == TokenType.PLUS)
            {
                return (int)node.Left.Visit(this) + (int)node.Right.Visit(this);
            }
            else if (node.Operation.TokenType == TokenType.MINUS)
            {
                return (int)node.Left.Visit(this) - (int)node.Right.Visit(this);
            }
            else if (node.Operation.TokenType == TokenType.MUL)
            {
                return (int)node.Left.Visit(this) * (int)node.Right.Visit(this);
            }
            else if (node.Operation.TokenType == TokenType.DIV)
            {
                return (int)node.Left.Visit(this) / (int)node.Right.Visit(this);
            }

            throw new Exception("Unknown BinOP TokenType");
        }

        public object Visit_Block(Block node)
        {
            foreach (var item in node.Declarations)
            {
                item.Visit(this);
            }

            foreach (var item in node.BodyStatements)
            {
                item.Visit(this);
            }

            return null; //TODO
        }

        public object Visit_VarDeclaration(VarDeclaration node)
        {
            return null; //TODO
        }

        public object Visit_Num(Num node)
        {
            return node.Value;
        }

        public object Visit_UnaryOp(UnaryOp node)
        {
            if (node.Token.TokenType == TokenType.PLUS)
            {
                return +(int)node.Expression.Visit(this);
            }
            else if (node.Token.TokenType == TokenType.MINUS)
            {
                return -(int)node.Expression.Visit(this);
            }

            throw new Exception("Unknown UnaryOP TokenType");
        }

        public object Visit_Var(Var node)
        {

            if (variables.TryGetValue(node.Value, out int result))
            {
                return result;
            }

            throw new Exception("Variable doesnt exist");
        }

        public object Visit_Condition(Condition condition)
        {
            throw new NotImplementedException();
        }

        public object Visit_FuncCallStatement(FuncCallStatement funcCallStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_WhileStatement(WhileStatement whileStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_IfStatement(IfStatement ifStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_ForStatement(ForStatement node)
        {
            throw new NotImplementedException();
        }
    }
}
