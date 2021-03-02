using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetonQuestEditorApp.Model.Compiler;

namespace BetonQuestEditorApp.Model
{
    public interface IVariableDefinition : IStatement
    {
        string VariableName { get; }
    }

    public interface ITypedVariableDefinition<T> : IVariableDefinition
    {
    }
}
