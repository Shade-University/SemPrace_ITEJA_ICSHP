using LanguageLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.Model
{
    public static class LanguageExecutor
    {
        private static Lexer lexer;
        private static Parser parser;
        private static Interpreter interpreter;

        public static string Compile(string code)
        {
            lexer = new Lexer(code);
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);

            interpreter.Interpret();

            return "Executed with no problem.";
        }

    }
}
