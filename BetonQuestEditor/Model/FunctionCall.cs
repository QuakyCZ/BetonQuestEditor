using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetonQuestEditorApp.Model.Compiler;

namespace BetonQuestEditorApp.Model
{
    public class FunctionCall : IStatement
    {
        public string FunctionName { get; set; }
        public List<IExpression> Parameters { get; } = new List<IExpression>();

        public string Compile(CompilerContext context)
        {
            return $"{FunctionName}({String.Join(", ", Parameters.Select(p => p.Compile(context)))})\n";
        }
    }
}
