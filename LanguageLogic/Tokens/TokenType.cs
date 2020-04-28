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
        QUOTE,
        TEXT,

        LESS_OR_EQUAL,
        MORE_OR_EQUAL,
        NOT_EQUAL,
        EQUALS,

        NUMBER,
        PLUS,
        MINUS,
        MUL,
        DIV,

        WRITE,
        PEN,
        ANGLE,
        FORWARD,
        BACKWARD,

        EOF
    }
}
