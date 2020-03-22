using LanguageLogic.AST;
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

        public IASTNode Parse()
        {
            IASTNode node = Program();
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

        private IASTNode Program()
        {
            IASTNode node = Block();
            EatToken(TokenType.END);
            return node;
        }

        private IASTNode Block()
        {
            List<IASTNode> declarations = Declarations();
            List<IASTNode> nodes = Statements();

            Block root = new Block(declarations, nodes);

            return root;
            
        }

        private List<IASTNode> Declarations()
        {
            List<IASTNode> results = new List<IASTNode>();
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

        private List<IASTNode> Statements()
        {
            List<IASTNode> results = new List<IASTNode>();

            results.Add(Statement());

            while(currentToken.TokenType != TokenType.END)
            {
                results.Add(Statement());
            }

            return results;
        }

        private IASTNode Statement()
        {
            //TODO
            IASTNode node = Assignment();

            return node;
        }

        private IASTNode Assignment()
        {
            IASTNode left = Variable();
            Token token = currentToken;
            EatToken(TokenType.ASSIGN);
            IASTNode right = Expression();

            IASTNode node = new Assign(left, right, token);
            return node;
        }

        private IASTNode Variable()
        {
            IASTNode node = new Var(currentToken);
            EatToken(TokenType.IDENT);
            return node;
        }
        private IASTNode Expression()
        {
            IASTNode node = Term();

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
            EatToken(TokenType.SEMICOLON);
            return node;
        }

        private IASTNode Term()
        {
            IASTNode node = Factor();

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

        private IASTNode Factor()
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
                IASTNode node = Expression();
                EatToken(TokenType.RPARENT);
                return node;
            }
            else
            {
                IASTNode node = Variable();
                return node;
            }
            throw new Exception("Factor did not return result");
        }
    }
}
