using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetonQuestEditorApp.Model.Compiler;

namespace BetonQuestEditorApp.Model
{
    public class StubStatement : IStatement
    {
        public string Compile(CompilerContext context)
        {
            return "";
        }
    }
}
