using LanguageLogic.AST;
using System;
using System.Linq;

namespace LanguageLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            string Example1 = @"
                    var first, second, temp, afterSwap, x;

                    afterSwap = ""After swap: First number 8, Second number: 5"";

                    func write(""First number: 5"")
                    func write(""Second number: 8"")
                    first = 5;
                    second = first + 3;
                    func write(afterSwap)

                    if first != second then
                    {
                        temp = first;
                        first = second;
                        second = temp;
                    }
                    func write(first)
                    func write(second)
                    func write(""\n"")

                    x = 0;
                    for 1 to 10 do
                    {
                        var y;
                        y = 1;

                        x = x + y;
                        if x / 2 == 1 then
                        { func write(x) }
                    }
            END.
             ";
            Lexer lexer = new Lexer(Example1);
            Parser parser = new Parser(lexer);
            Interpreter interpreter = new Interpreter(parser);
            interpreter.Interpret();

            Console.WriteLine("Completed");
            Console.ReadKey();
        }
    }
}
