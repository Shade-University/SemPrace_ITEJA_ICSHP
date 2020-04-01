using LanguageLogic.AST;
using LanguageLogic.AST.Statements;
using LanguageLogic.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

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
                throw new Exception("Error");

            return node;
        }

        private void EatToken(TokenType tokenType)
        {
            if (currentToken.TokenType == tokenType)
                currentToken = lexer.GetNextToken();
            else
                throw new Exception("Token type does not match");
        }

        private Block Program()
        {
            Block node = Block();
            EatToken(TokenType.END);
            return node;
        }

        private Block Block()
        {
            List<Var> declarations = Declarations();
            List<IStatement> statements = Statements();

            Block root = new Block(declarations, statements);

            return root;
            
        }

        private List<Var> Declarations()
        {
            List<Var> results = new List<Var>();
            if(currentToken.TokenType == TokenType.VAR)
            {
                EatToken(TokenType.VAR);

                while(currentToken.TokenType == TokenType.IDENT)
                {
                    results.Add(new Var(currentToken));
                    EatToken(TokenType.IDENT);

                    if (currentToken.TokenType == TokenType.COMA)
                        EatToken(TokenType.COMA);
                }
                EatToken(TokenType.SEMICOLON);
            }

            return results;
        }

        private List<IStatement> Statements()
        {
            List<IStatement> results = new List<IStatement>();

            while(currentToken.TokenType != TokenType.END)
            {
                results.Add(Statement());
            }

            return results;
        }

        private IStatement Statement()
        {
            switch(currentToken.TokenType)
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

            List<IStatement> statements = new List<IStatement>();
            while (currentToken.TokenType != TokenType.RBRACKET)
            {
                statements.Add(Statement());
            }

            EatToken(TokenType.RBRACKET);
            return new ForStatement(left, statements, right);
        }

        private IStatement WhileStatement()
        {
            EatToken(TokenType.WHILE);
            Condition condition = Condition();
            EatToken(TokenType.DO);
            EatToken(TokenType.LBRACKET);

            List<IStatement> statements = new List<IStatement>();
            while (currentToken.TokenType != TokenType.RBRACKET)
            {
                statements.Add(Statement());
            }

            EatToken(TokenType.RBRACKET);
            return new WhileStatement(condition, statements);
        }

        private IStatement IfStatement()
        {
            EatToken(TokenType.IF);
            Condition condition = Condition();
            EatToken(TokenType.THEN);
            EatToken(TokenType.LBRACKET);

            List<IStatement> statements = new List<IStatement>();
            while (currentToken.TokenType != TokenType.RBRACKET)
            {
                statements.Add(Statement());
            }

            EatToken(TokenType.RBRACKET);
            return new IfStatement(condition, statements);
        }

        private IStatement FuncCallStatement()
        {
            throw new NotImplementedException();
        }

        private IStatement AssignStatement()
        {
            string ident = currentToken.Value;
            EatToken(TokenType.IDENT);
            Token token = currentToken;
            EatToken(TokenType.ASSIGN);
            IExpression right = Expression();
            EatToken(TokenType.SEMICOLON);

            IStatement node = new AssignStatement(ident, right, token);
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

        #region NUMBER_EXPRESSION
        private IExpression Expression()
        {
            IExpression node = Term();

            while (currentToken.TokenType == TokenType.PLUS ||
                currentToken.TokenType == TokenType.MINUS)
            {
                Token token = currentToken;
                if(token.TokenType == TokenType.PLUS)
                    EatToken(TokenType.PLUS);
                else if(token.TokenType == TokenType.MINUS)
                    EatToken(TokenType.MINUS);

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
                    EatToken(TokenType.MUL);
                else if (token.TokenType == TokenType.DIV)
                    EatToken(TokenType.DIV);

                node = new BinOp(node, token, Factor());
            }

            return node;
        }
        private IExpression Factor()
        {
            Token token = currentToken;

            if(token.TokenType == TokenType.PLUS)
            {
                EatToken(TokenType.PLUS);
                return new UnaryOp(token, Factor());
            }
            else if (token.TokenType == TokenType.MINUS)
            {
                EatToken(TokenType.MINUS);
                return new UnaryOp(token, Factor());
            }
            else if(token.TokenType == TokenType.NUMBER)
            {
                EatToken(TokenType.NUMBER);
                return new Num(token);
            }
            else if(token.TokenType == TokenType.LPARENT)
            {
                EatToken(TokenType.LPARENT);
                IExpression node = Expression();
                EatToken(TokenType.RPARENT);
                return node;
            }
            else
            {
                IExpression node = Variable();
                return node;
            }
            throw new Exception("Factor did not return result");
        }
        #endregion
    }
}
