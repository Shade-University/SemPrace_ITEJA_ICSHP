using LanguageLogic.AST;
using LanguageLogic.AST.Statements;
using LanguageLogic.AST.Statements.Functions;
using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;

namespace LanguageLogic
{
    public delegate void AngleDelegate(object angle);
    public delegate void BackwardDelegate(object move);
    public delegate void ForwardDelegate(object move);
    public delegate void PenDelegate(PenStatus status);
    public delegate void WriteDelegate(object text); //i would use just inheritance and rewrite interpret methods of these functions, but one of condition
    //is to use delegates, so i used it here
    //I didnt create GUI to console

    public class Interpreter : INodeVisitor
    {
        public AngleDelegate AngleDelegate { get; set; }
        public BackwardDelegate BackwardDelegate { get; set; }
        public ForwardDelegate ForwardDelegate { get; set; }
        public PenDelegate PenDelegate { get; set; } //Not implemented to console
        public WriteDelegate WriteDelegate { get; set; } = delegate (object text)
        {
            Console.WriteLine(text);
        }; //Default anonymous method to write to console

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
            foreach (ExecutionContext item in context)
            {
                if (item.VariableExist(node.Variable.Identifier))
                {
                    object value = node.Expression.Visit(this);
                    item.AssignVariable(node.Variable.Identifier, value);

                    if (value is string)
                    {
                        node.Variable.Type = VarType.STRING;
                    }
                    else if (value is double)
                    {
                        node.Variable.Type = VarType.DOUBLE;
                    } //Not working, but it quite doesnt matter. Object can handle any type

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
            context.Push(new ExecutionContext()); //New context
            foreach (VarDeclaration item in node.Declarations)
            {
                item.Visit(this);
            }

            foreach (IStatement item in node.BodyStatements)
            {
                item.Visit(this);
            }
            context.Pop(); //Remove last context

            return null;
        }

        public object Visit_VarDeclaration(VarDeclaration node)
        {
            if (context.Peek().VariableExist(node.Variable.Identifier))
            {
                throw new Exception("Variable already exist");
            }

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
            foreach (ExecutionContext item in context)
            {
                if (item.VariableExist(node.Identifier))
                {
                    return item.GetVariable(node.Identifier);
                }
            }
            //Pokus se získat proměnnou
            return null;
        }

        public object Visit_Condition(Condition condition)
        {
            switch (condition.Token.TokenType)
            {
                case TokenType.EQUALS:
                    return condition.Left.Visit(this).Equals(condition.Right.Visit(this)); //Because of strings
                case TokenType.LESS_OR_EQUAL:
                    return (double)condition.Left.Visit(this) <= (double)condition.Right.Visit(this);
                case TokenType.MORE_OR_EQUAL:
                    return (double)condition.Left.Visit(this) >= (double)condition.Right.Visit(this);
                case TokenType.LESS:
                    return (double)condition.Left.Visit(this) < (double)condition.Right.Visit(this);
                case TokenType.MORE:
                    return (double)condition.Left.Visit(this) > (double)condition.Right.Visit(this); //This can be done only with numbers
                case TokenType.NOT_EQUAL:
                    return !condition.Left.Visit(this).Equals(condition.Right.Visit(this)); //because of strings
                default:
                    throw new Exception("Invalid condition token");
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
                whileStatement.BodyBlock.Visit(this);
            }

            return null;
        }

        public object Visit_IfStatement(IfStatement ifStatement)
        {
            if ((bool)ifStatement.Condition.Visit(this))
            {
                ifStatement.BodyBlock.Visit(this);
            }

            return null;
        }

        public object Visit_ForStatement(ForStatement node)
        {
            double from = (double)node.FromExpression.Visit(this);
            for (; from < (double)node.ToExpression.Visit(this); from++)
            {
                node.BodyBlock.Visit(this);
            }

            return null;
        }

        public object Visit_AngleStatement(AngleStatement angleStatement)
        {
            AngleDelegate(angleStatement.Angle.Visit(this));

            return null;
        }

        public object Visit_BackwardStatement(BackwardStatement backwardStatement)
        {
            BackwardDelegate(backwardStatement.Expression.Visit(this));

            return null;
        }

        public object Visit_ForwardStatement(ForwardStatement forwardStatement)
        {
            ForwardDelegate(forwardStatement.Expression.Visit(this));

            return null;
        }

        public object Visit_WriteStatement(WriteStatement writeStatement)
        {
            WriteDelegate(writeStatement.Expression.Visit(this));

            return null;
        }

        public object Visit_PenStatement(PenStatement penStatement)
        {
            PenDelegate(penStatement.PenStatus);

            return null;
        }

        public object Visit_StringText(StringText stringText)
        {
            return stringText.Text;
        }
    }
}
