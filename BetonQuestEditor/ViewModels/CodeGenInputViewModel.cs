using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetonQuestEditorApp.Views;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;

namespace BetonQuestEditorApp.ViewModels
{
    public class CodeGenInputViewModel<T> : ValueNodeInputViewModel<T>
    {
        static CodeGenInputViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeInputView(), typeof(IViewFor<CodeGenInputViewModel<T>>));
        }

        public CodeGenInputViewModel(PortType type)
        {
            this.Port = new CodeGenPortViewModel { PortType = type };

            if (type == PortType.Execution)
            {
                this.PortPosition = PortPosition.Right;
            }
        }
    }
}
