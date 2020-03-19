using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLogic
{
    public class Token
    {
        public TokenType TokenType { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"Token[{TokenType}, {Value}]";
        }
    }
}
