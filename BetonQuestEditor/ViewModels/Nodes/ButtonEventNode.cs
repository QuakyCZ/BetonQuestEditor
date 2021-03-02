using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using BetonQuestEditorApp.Model.Compiler;
using BetonQuestEditorApp.Views;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using ReactiveUI;

namespace BetonQuestEditorApp.ViewModels.Nodes
{
    public class ButtonEventNode : CodeGenNodeViewModel
    {
        static ButtonEventNode()
        {
            Splat.Locator.CurrentMutable.Register(() => new CodeGenNodeView(), typeof(IViewFor<ButtonEventNode>));
        }

        public ValueListNodeInputViewModel<IStatement> OnClickFlow { get; }

        public ButtonEventNode() : base(NodeType.EventNode)
        {
            this.Name = "Button Events";

            OnClickFlow = new CodeGenListInputViewModel<IStatement>(PortType.Execution)
            {
                Name = "On Click"
            };

            this.Inputs.Add(OnClickFlow);
        }
    }
}
