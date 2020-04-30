namespace LanguageLogic.AST.Statements.Functions
{
    public enum PenStatus
    {
        UP, DOWN
    }
    public class PenStatement : IStatement
    {
        public PenStatus PenStatus { get; set; } = PenStatus.DOWN; //Default is down -> DOWN = Draw, UP (pero is up) not draw

        public PenStatement(PenStatus penStatus)
        {
            PenStatus = penStatus;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_PenStatement(this);
        }
    }
}
