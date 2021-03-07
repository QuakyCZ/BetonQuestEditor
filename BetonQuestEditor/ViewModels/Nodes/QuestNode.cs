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
using System.Drawing;

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

        // ViewModel for the button
        public BoolValueEditorViewModel BoolEditor { get; } = new BoolValueEditorViewModel();

        //public GroupEndpointEditorViewModel<int> EndpointEditor { get; } = new GroupEndpointEditorViewModel<int>();

        public ValueNodeOutputViewModel<ITypedExpression<string>> Output { get; }

        public QuestNode() : base(NodeType.Literal)
        {
            this.Name = "Mungo";
            
             // Create task for loading HeaderIcon
             Task<Task> task = new Task<Task>(async () => {
                 // TODO: Replace hardcoded path with user selected value
                var fileStream = new FileStream(@"Resources\Images\Mongo.jpg", FileMode.Open);
              
                 //TODO: Use correct image dependant values for width and height. Size of the icon is determined elsewhere
                 this.HeaderIcon = await Splat.BitmapLoader.Current.Load(fileStream, 2*163, 2*344);
              
                fileStream.Close();
            });

            // Run the task
            task.Start();
            task.Wait();
            task.Result.Wait();

            Output = new CodeGenOutputViewModel<ITypedExpression<string>>(PortType.String)
            {
                Name = "Value1",
                Editor = ValueEditor,
                Value = ValueEditor.ValueChanged.Select(v => new StringLiteral{ Value = v })
            };
            this.Outputs.Add(Output);

            // Button; TODO make button do some usefull job
            Output = new CodeGenOutputViewModel<ITypedExpression<string>>(PortType.Boolean)
            {
                Name = "ValueB",
                Editor = BoolEditor//,
                //Value = BoolEditor.ValueChanged.Select(v => new BoolLiteral{ Value = v })
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
