using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic
{
    public class Interpreter
    {
        private Parser parser;
        public Interpreter(Parser parser)
        {
            this.parser = parser;
        }

        public void Interpret()
        {
            BinOp rootNode = (BinOp) parser.Expression();

            double output = rootNode.Visit();
        }
    }
}
