﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.AST
{
    public class Var : IASTNode
    {
        public string Value { get; }
        public Token Token { get; }

        public Var(Token token)
        {
            Value = token.Value;
            Token = token;
        }

        public override object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Var(this);
        }
    }
}