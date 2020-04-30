using System.Collections.Generic;

namespace LanguageLogic
{
    public class ExecutionContext
    {
        private Dictionary<string, object> ReservedVariables; //Object or generic T
        //Store variables in memory

        public ExecutionContext()
        {
            ReservedVariables = new Dictionary<string, object>();
        }

        public bool VariableExist(string ident)
        {
            return ReservedVariables.ContainsKey(ident);
        }

        public void DeclareVariable(string ident)
        {
            ReservedVariables[ident] = null;
        }

        public void AssignVariable(string ident, object value)
        {
            ReservedVariables[ident] = value;
        }

        public object GetVariable(string ident)
        {
            if (ReservedVariables.TryGetValue(ident, out object value))
            {
                return value;
            }

            throw new System.Exception("Variable not declared");

        }

    }
}