using LanguageLogic.AST;
using System;
using System.Linq;

namespace LanguageLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            string my_lang = @"
            var x, y;

            x = 10 - 5;
            y = 0;
            for 1 to x do
            {
	            y = y + 1;
            }
            if y < 51 then 
            {
            }

                    END

             ";
            Lexer lexer = new Lexer(my_lang);
            Parser parser = new Parser(lexer);
            /*Interpreter interpreter = new Interpreter(parser);
            interpreter.Interpret();*/

            Block node = parser.Parse();

            Console.ReadKey();
        }
    }
}
