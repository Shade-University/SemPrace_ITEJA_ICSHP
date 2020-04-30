using LanguageLogic.AST;
using LanguageLogic.AST.Statements;
using LanguageLogic.AST.Statements.Functions;
using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;

namespace LanguageLogic
{
    public class Parser
    {
        private Lexer lexer;
        private Token currentToken;
        public Parser(Lexer lexer)
        {
            this.lexer = lexer; //Dependency injection
            currentToken = lexer.GetNextToken();
        }

        public Block Parse()
        {
            Block node = Program();
            if (currentToken.TokenType != TokenType.EOF)
            {
                throw new Exception("Error");
            }

            return node;
        }

        private void EatToken(TokenType tokenType) //Should add string parameter with error message.
        {
            if (currentToken.TokenType == tokenType)
            {
                currentToken = lexer.GetNextToken();
            }
            else
            {
                throw new Exception("Token type does not match");
            }
        }

        private Block Program()
        {
            Block node = Block();
            EatToken(TokenType.END);
            EatToken(TokenType.DOT);
            return node;
        }

        private Block Block()
        {
            List<VarDeclaration> declarations = Declarations();
            List<IStatement> statements = Statements();

            Block root = new Block(declarations, statements);

            return root;

        }

        private List<VarDeclaration> Declarations()
        {
            List<VarDeclaration> results = new List<VarDeclaration>();
            if (currentToken.TokenType == TokenType.VAR)
            {
                EatToken(TokenType.VAR);

                while (currentToken.TokenType == TokenType.IDENT)
                {
                    results.Add(new VarDeclaration(Variable()));

                    if (currentToken.TokenType == TokenType.COMA)
                    {
                        EatToken(TokenType.COMA);
                    }
                }
                EatToken(TokenType.SEMICOLON);
            }

            return results;
        }

        private List<IStatement> Statements()
        {
            List<IStatement> results = new List<IStatement>();

            while (currentToken.TokenType != TokenType.END && currentToken.TokenType != TokenType.RBRACKET)
            {
                results.Add(Statement());
            }

            return results;
        }

        private IStatement Statement()
        {
            switch (currentToken.TokenType)
            {
                case TokenType.IDENT: return AssignStatement();
                case TokenType.FUNC: return FuncCallStatement();
                case TokenType.IF: return IfStatement();
                case TokenType.WHILE: return WhileStatement();
                case TokenType.FOR: return ForStatement();
            }

            throw new Exception("Expected valid statement token");
        }

        private IStatement ForStatement()
        {
            EatToken(TokenType.FOR);
            IExpression left = Expression();
            EatToken(TokenType.TO);
            IExpression right = Expression();
            EatToken(TokenType.DO);
            EatToken(TokenType.LBRACKET);
            Block block = Block();
            EatToken(TokenType.RBRACKET);
            return new ForStatement(left, block, right);
        }

        private IStatement WhileStatement()
        {
            EatToken(TokenType.WHILE);
            Condition condition = Condition();
            EatToken(TokenType.DO);
            EatToken(TokenType.LBRACKET);

            Block block = Block();

            EatToken(TokenType.RBRACKET);
            return new WhileStatement(condition, block);
        }

        private IStatement IfStatement()
        {
            EatToken(TokenType.IF);
            Condition condition = Condition();
            EatToken(TokenType.THEN);
            EatToken(TokenType.LBRACKET);

            Block block = Block();

            EatToken(TokenType.RBRACKET);
            return new IfStatement(condition, block);
        }

        private IStatement FuncCallStatement()
        {
            EatToken(TokenType.FUNC);

            switch (currentToken.TokenType)
            {
                case TokenType.FORWARD:
                    return ForwardStatement();
                case TokenType.BACKWARD:
                    return BackwardStatement();
                case TokenType.PEN:
                    return PenStatement();
                case TokenType.ANGLE:
                    return AngleStatement();
                case TokenType.WRITE:
                    return WriteStatement();
                default:
                    throw new Exception("Unknown function call");
            }

        }

        private IStatement WriteStatement()
        {
            EatToken(TokenType.WRITE);
            EatToken(TokenType.LPARENT);
            WriteStatement writeStatement = new WriteStatement(Expression());
            EatToken(TokenType.RPARENT);

            return writeStatement;
        }

        private IStatement AngleStatement()
        {
            EatToken(TokenType.ANGLE);
            EatToken(TokenType.LPARENT);
            AngleStatement angleStatement = new AngleStatement(Expression());
            EatToken(TokenType.RPARENT);

            return angleStatement;
        }

        private IStatement PenStatement()
        {
            EatToken(TokenType.PEN);
            EatToken(TokenType.LPARENT);
            PenStatement penStatement = new PenStatement(PenStatusValue());
            EatToken(TokenType.RPARENT);

            return penStatement;
        }

        private PenStatus PenStatusValue()
        {
            if(currentToken.TokenType == TokenType.UP || currentToken.TokenType == TokenType.DOWN)
            {
                PenStatus penStatus = currentToken.TokenType == TokenType.UP ? PenStatus.UP : PenStatus.DOWN;
                EatToken(currentToken.TokenType);
                return penStatus;
            }

            throw new Exception("Invalid Pen status");
        }

        private IStatement BackwardStatement()
        {
            EatToken(TokenType.BACKWARD);
            EatToken(TokenType.LPARENT);
            BackwardStatement backwardStatement = new BackwardStatement(Expression());
            EatToken(TokenType.RPARENT);

            return backwardStatement;
        }

        private IStatement ForwardStatement()
        {
            EatToken(TokenType.FORWARD);
            EatToken(TokenType.LPARENT);
            ForwardStatement forwardStatement = new ForwardStatement(Expression());
            EatToken(TokenType.RPARENT);

            return forwardStatement;
        }

        private IStatement AssignStatement()
        {
            Var var = Variable();
            Token token = currentToken;
            EatToken(TokenType.ASSIGN);
            IExpression right = Expression();
            EatToken(TokenType.SEMICOLON);

            IStatement node = new AssignStatement(var, right, token);
            return node;
        }

        private Var Variable()
        {
            Var node = new Var(currentToken);
            EatToken(TokenType.IDENT);
            return node;
        }

        private Condition Condition()
        {
            IExpression left = Expression();
            Token token = currentToken; //Condition (<,>...)
            EatToken(token.TokenType);
            IExpression right = Expression();

            return new Condition(left, token, right);
        }

        #region EXPRESSION
        private IExpression Expression() //Expression can be string, variable or some mathematical expression
        {
            IExpression node = Term();

            while (currentToken.TokenType == TokenType.PLUS ||
                currentToken.TokenType == TokenType.MINUS)
            {
                Token token = currentToken;
                if (token.TokenType == TokenType.PLUS)
                {
                    EatToken(TokenType.PLUS);
                }
                else if (token.TokenType == TokenType.MINUS)
                {
                    EatToken(TokenType.MINUS);
                }

                node = new BinOp(node, token, Term());
            }
            return node;
        }
        private IExpression Term()
        {
            IExpression node = Factor();

            while (currentToken.TokenType == TokenType.MUL ||
                currentToken.TokenType == TokenType.DIV)
            {
                Token token = currentToken;
                if (token.TokenType == TokenType.MUL)
                {
                    EatToken(TokenType.MUL);
                }
                else if (token.TokenType == TokenType.DIV)
                {
                    EatToken(TokenType.DIV);
                }

                node = new BinOp(node, token, Factor());
            }

            return node;
        }
        private IExpression Factor()
        {
            Token token = currentToken;

            if (token.TokenType == TokenType.PLUS)
            {
                EatToken(TokenType.PLUS);
                return new UnaryOp(token, Factor());
            }
            else if (token.TokenType == TokenType.MINUS)
            {
                EatToken(TokenType.MINUS);
                return new UnaryOp(token, Factor());
            }
            else if (token.TokenType == TokenType.NUMBER)
            {
                EatToken(TokenType.NUMBER);
                return new Num(token);
            }
            else if (token.TokenType == TokenType.IDENT)
            {
                IExpression node = Variable();
                return node;
            }
            else if (token.TokenType == TokenType.LPARENT)
            {
                EatToken(TokenType.LPARENT);
                IExpression node = Expression();
                EatToken(TokenType.RPARENT);
                return node;
            }
            else if (token.TokenType == TokenType.TEXT)
            {
                IExpression text = new StringText(currentToken);
                EatToken(TokenType.TEXT);
                return text;
            }

            throw new Exception("Factor did not return result");
        }
        #endregion
    }
}
