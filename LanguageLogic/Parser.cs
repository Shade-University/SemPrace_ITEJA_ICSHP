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

        private void EatToken(TokenType tokenType)
        {
            if (currentToken.TokenType == tokenType)
                currentToken = lexer.GetNextToken();
            else
                throw new Exception("Token type does not match");
        }

        public IASTNode Expression()
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
            throw new Exception("Factor did not return result");
        }
    }
}
