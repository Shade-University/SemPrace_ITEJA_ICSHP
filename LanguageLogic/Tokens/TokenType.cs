using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic.Tokens
{
    public enum TokenType
    {
        END,
        VAR,
        IDENT,
        FUNC,
        IF,
        THEN,
        WHILE,
        DO,
        FOR,
        TO,

        LBRACKET,
        RBRACKET,
        LPARENT,
        RPARENT,
        DOT,
        COMA,
        SEMICOLON,
        ASSIGN,
        LESS,
        MORE,
        LQUOTE,
        RQUOTE,

        LESS_OR_EQUAL,
        MORE_OR_EQUAL,
        EQUALS,

        NUMBER,
        PLUS,
        MINUS,
        MUL,
        DIV,

        EOF
    }
}
