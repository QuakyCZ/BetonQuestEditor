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
using System.Windows.Media.Imaging;
using ReactiveUI;
using System.IO;

namespace BetonQuestEditorApp.ViewModels.Nodes
{
    /// <summary>
    ///  Primary Quest node. To be used for assigning tasks for questers
    /// </summary>

    public class QuestNode : CodeGenNodeViewModel
    {
        static QuestNode()
        {
            Splat.Locator.CurrentMutable.Register(() => new CodeGenNodeView(), typeof(IViewFor<QuestNode>));
        }


        public StringValueEditorViewModel ValueEditor { get; } = new StringValueEditorViewModel();
        public StringValueEditorViewModel ValueEditor1 { get; } = new StringValueEditorViewModel();
        public StringValueEditorViewModel ValueEditor2 { get; } = new StringValueEditorViewModel();

        private NpcChooseDatagridViewModel NPCChooseEditor { get; } = new NpcChooseDatagridViewModel();

        // ViewModel for the button
        public BoolValueEditorViewModel BoolEditor { get; } = new BoolValueEditorViewModel();

        public EditableLabelEditorViewModel<string> EditableLabelEditor { get; } = new EditableLabelEditorViewModel<string>("defName");

        public ValueNodeInputViewModel<ITypedExpression<string>> Text { get; }

        public ValueNodeOutputViewModel<ITypedExpression<string>> Output { get; }


        /// <summary>
        /// Sets current NPC according the selected value in model
        /// </summary>
        /// <param></param>
        void SelectNPC()
        {
            try
            {
                var fileStream = new FileStream(NPCChooseEditor.NpcValue.FilePath, FileMode.Open);
                HeaderIcon = Splat.BitmapLoader.Current.Load(fileStream, 2 * 163, 2 * 344).Result;
                fileStream.Close();
                Name = NPCChooseEditor.NpcValue.Name;
            }
            catch (Exception ex) when (ex is NullReferenceException || ex is InvalidOperationException || ex is FileNotFoundException)
            { 
                
            }
        }

        public QuestNode() : base(NodeType.Literal)
        {
            this.Name = NPCChooseEditor.NpcValue.Name;

            // HeaderIconButton action
            this.HeaderIconButton = ReactiveCommand.Create(() =>
            {
                // Call models function
                NPCChooseEditor.NpcSelectNew();
            });

            // Watch models selected value for change. Once changed, update the nodes icon
            this.WhenAnyValue(x => x.NPCChooseEditor.NpcValue).Subscribe(name => { SelectNPC(); });

            // Button; TODO make button do some usefull job
            Output = new CodeGenOutputViewModel<ITypedExpression<string>>(PortType.Boolean)
            {
                Name = "ValueB",
                Editor = BoolEditor//,
                //Value = BoolEditor.ValueChanged.Select(v => new BoolLiteral{ Value = v })
            };
            this.Outputs.Add(Output);

            Text = new CodeGenInputViewModel<ITypedExpression<string>>(PortType.String)
            {
                Editor = EditableLabelEditor
            };
            this.Inputs.Add(Text);


            Output = new CodeGenOutputViewModel<ITypedExpression<string>>(PortType.String)
            {
                Name = "Value1",
                Editor = ValueEditor,
                Value = ValueEditor.ValueChanged.Select(v => new StringLiteral{ Value = v })
            };
            this.Outputs.Add(Output);

            Output = new CodeGenOutputViewModel<ITypedExpression<string>>(PortType.String)
            {
                Name = "Value2",
                Editor = ValueEditor1,
                Value = ValueEditor.ValueChanged.Select(v => new StringLiteral { Value = v })
            };
            this.Outputs.Add(Output);

            Output = new CodeGenOutputViewModel<ITypedExpression<string>>(PortType.String)
            {
                Name = "Value3",
                Editor = ValueEditor2,
                Value = ValueEditor.ValueChanged.Select(v => new StringLiteral { Value = v })
            };
            this.Outputs.Add(Output);

        }
    }
}
