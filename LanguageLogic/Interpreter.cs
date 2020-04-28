using LanguageLogic.AST;
using LanguageLogic.AST.Statements;
using LanguageLogic.AST.Statements.Functions;
using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic
{
    public class Interpreter : INodeVisitor
    {
        private Parser parser;
        private Stack<ExecutionContext> context;
        public Interpreter(Parser parser)
        {
            this.parser = parser;
            context = new Stack<ExecutionContext>();
        }


        public void Interpret()
        {
            context.Push(new ExecutionContext());
            Block block = parser.Parse();
            block.Visit(this);
        }

        public object Visit_Assign(AssignStatement node)
        {
            foreach (var item in context)
            {
                if(item.VariableExist(node.Variable.Identifier))
                {
                    var value = node.Expression.Visit(this);
                    item.AssignVariable(node.Variable.Identifier, value);

                    if (value is string)
                        node.Variable.Type = VarType.STRING;
                    else if (value is double)
                        node.Variable.Type = VarType.DOUBLE;

                    return null;
                }
            }

            throw new Exception("Cant assign to non-existing variable");
        }

        public object Visit_BinOp(BinOp node)
        {
            if (node.Operation.TokenType == TokenType.PLUS)
            {
                return (double)node.Left.Visit(this) + (double)node.Right.Visit(this);
            }
            else if (node.Operation.TokenType == TokenType.MINUS)
            {
                return (double)node.Left.Visit(this) - (double)node.Right.Visit(this);
            }
            else if (node.Operation.TokenType == TokenType.MUL)
            {
                return (double)node.Left.Visit(this) * (double)node.Right.Visit(this);
            }
            else if (node.Operation.TokenType == TokenType.DIV)
            {
                return (double)node.Left.Visit(this) / (double)node.Right.Visit(this);
            }

            throw new Exception("Unknown BinOP TokenType");
        }

        public object Visit_Block(Block node)
        {
            context.Push(new ExecutionContext()); //Vytvoř nový kontext
            foreach (var item in node.Declarations)
            {
                item.Visit(this);
            }

            foreach (var item in node.BodyStatements)
            {
                item.Visit(this);
            }
            context.Pop(); //Odstraň kontext

            return null;
        }

        public object Visit_VarDeclaration(VarDeclaration node)
        {
            if (context.Peek().VariableExist(node.Variable.Identifier))
                throw new Exception("Variable already exist");

            context.Peek().DeclareVariable(node.Variable.Identifier);
            return null;
        }

        public object Visit_Num(Num node)
        {
            return node.Value;
        }

        public object Visit_UnaryOp(UnaryOp node)
        {
            if (node.Token.TokenType == TokenType.PLUS)
            {
                return +(double)node.Expression.Visit(this);
            }
            else if (node.Token.TokenType == TokenType.MINUS)
            {
                return -(double)node.Expression.Visit(this);
            }

            throw new Exception("Unknown UnaryOP TokenType");
        }

        public object Visit_Var(Var node)
        {
            foreach (var item in context)
            {
                if (item.VariableExist(node.Identifier))
                    return item.GetVariable(node.Identifier);
            }
            //Pokus se získat proměnnou
            return null;
        }

        public object Visit_Condition(Condition condition)
        {
            switch (condition.Token.TokenType)
            {
                case TokenType.EQUALS:
                    return condition.Left.Visit(this).Equals(condition.Right.Visit(this));
                case TokenType.LESS_OR_EQUAL:
                    return (double)condition.Left.Visit(this) <= (double)condition.Right.Visit(this);
                case TokenType.MORE_OR_EQUAL:
                    return (double)condition.Left.Visit(this) >= (double)condition.Right.Visit(this);
                case TokenType.LESS:
                    return (double)condition.Left.Visit(this) < (double)condition.Right.Visit(this);
                case TokenType.MORE:
                    return (double)condition.Left.Visit(this) > (double)condition.Right.Visit(this);
                case TokenType.NOT_EQUAL:
                    return !condition.Left.Visit(this).Equals(condition.Right.Visit(this));
                default:
                    throw new Exception("Invalid condition token"); //Podmínky upravit i na text
            }
        }

        public object Visit_FuncCallStatement(FuncCallStatement funcCallStatement)
        {
            funcCallStatement.Function.Visit(this);

            return null;
        }

        public object Visit_WhileStatement(WhileStatement whileStatement)
        {
            while ((bool)whileStatement.Condition.Visit(this))
            {
                context.Push(new ExecutionContext());
                whileStatement.BodyStatements.ForEach(x => x.Visit(this));
                context.Pop();
            }

            return null;
        }

        public object Visit_IfStatement(IfStatement ifStatement)
        {
            if ((bool)ifStatement.Condition.Visit(this))
            {
                context.Push(new ExecutionContext());
                ifStatement.BodyStatements.ForEach(x => x.Visit(this));
                context.Pop();
            }

            return null;
        }

        public object Visit_ForStatement(ForStatement node)
        {
            double from = (double)node.FromExpression.Visit(this);
            for (; from < (double)node.ToExpression.Visit(this); from++)
            {
                context.Push(new ExecutionContext());
                node.BodyStatements.ForEach(x => x.Visit(this));
                context.Pop();
            }

            return null;
        }

        public object Visit_AngleStatement(AngleStatement angleStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_BackwardStatement(BackwardStatement backwardStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_ForwardStatement(ForwardStatement forwardStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_WriteStatement(WriteStatement writeStatement)
        {
            Console.WriteLine(writeStatement.Expression.Visit(this));

            return null;
        }

        public object Visit_PenStatement(PenStatement penStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_StringText(StringText stringText)
        {
            return stringText.Text;
        }
    }
}
