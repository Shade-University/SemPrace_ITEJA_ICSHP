using LanguageLogic.Tokens;

namespace LanguageLogic.AST
{
    public class StringText : IExpression //Node representing text string
    {
        public string Text { get; }
        public Token Token { get; }
        public StringText(Token token)
        {
            Text = token.Value;
            Token = token;
        }
        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_StringText(this);
        }
    }
}
