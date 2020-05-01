using GUI.Services;
using LanguageLogic;

namespace GUI.Model
{
    public static class LanguageExecutor
    {
        private static Lexer lexer;
        private static Parser parser;
        private static Interpreter interpreter;

        public static string Compile(string code, DrawingService service) //Compile, setup delegates and interpret
        {
            lexer = new Lexer(code);
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);
            //we need to recreate 

            interpreter.AngleDelegate = service.Angle;
            interpreter.BackwardDelegate = service.Backward;
            interpreter.ForwardDelegate = service.Forward;
            interpreter.PenDelegate = service.Pen;
            interpreter.WriteDelegate = service.Write; //Assign service methods as delegates

            interpreter.Interpret();

            return "Executed with no problem.";
        }

    }
}
