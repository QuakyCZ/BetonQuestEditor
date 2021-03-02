using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using BetonQuestEditorApp.Model;
using BetonQuestEditorApp.Model.Compiler;
using BetonQuestEditorApp.ViewModels.Editors;
using BetonQuestEditorApp.Views;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace BetonQuestEditorApp.ViewModels.Nodes
{
    public class TextLiteralNode : CodeGenNodeViewModel
    {
        static TextLiteralNode()
        {
            Splat.Locator.CurrentMutable.Register(() => new CodeGenNodeView(), typeof(IViewFor<TextLiteralNode>));
        }

        public StringValueEditorViewModel ValueEditor { get; } = new StringValueEditorViewModel();

        public ValueNodeOutputViewModel<ITypedExpression<string>> Output { get; }

        public TextLiteralNode() : base(NodeType.Literal)
        {
            this.Name = "Text";

            Output = new CodeGenOutputViewModel<ITypedExpression<string>>(PortType.String)
            {
                Name = "Value",
                Editor = ValueEditor,
                Value = ValueEditor.ValueChanged.Select(v => new StringLiteral{ Value = v })
            };
            this.Outputs.Add(Output);
        }
    }
}
