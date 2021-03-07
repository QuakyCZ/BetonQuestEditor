using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetonQuestEditorApp.Model.Compiler;

namespace BetonQuestEditorApp.Model
{
    /// <summary>
    /// Class for representing a boolean type of control
    /// </summary>
    public class BoolLiteral : ITypedExpression<bool>
    {
        public bool Value { get; set; }

        public string Compile(CompilerContext ctx)
        {
            // TODO: return something meaningful

            return $"Juhuuu"; //\"{Value}\""; // Dummy output
        }
    }
}
