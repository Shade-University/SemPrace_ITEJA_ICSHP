using LanguageLogic.Tokens;

namespace LanguageLogic.AST.Statements
{
    public class AssignStatement : IStatement
    {
        public Var Variable { get; } // Variable
        public IExpression Expression { get; } //Expression to assign to variable
        public Token Token { get; } // :=

        public AssignStatement(Var variable, IExpression expression, Token token)
        {
            Variable = variable;
            Expression = expression;
            Token = token;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Assign(this);
        }
    }
}
