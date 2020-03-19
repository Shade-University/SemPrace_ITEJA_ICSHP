using System;
using System.Linq;

namespace LanguageLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer lexer = new Lexer("5 / 2");
            Parser parser = new Parser(lexer);
            Interpreter interpreter = new Interpreter(parser);
            interpreter.Interpret();

            Console.ReadKey();
        }
    }
}
