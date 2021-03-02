using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetonQuestEditorApp.Model.Compiler;

namespace BetonQuestEditorApp.Model
{
    public class IntLiteral : ITypedExpression<int>
    {
        public int Value { get; set; }

        public string Compile(CompilerContext context)
        {
            return Value.ToString();
        }
    }
}
