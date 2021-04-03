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

    public class NPC : ReactiveObject
    {
        public int id { get; }
        public int game_id { get; }
        public string Name { get; }
        public string FilePath { get; }

        public NPC(int id, int game_id, string name, string filepath)
        {
            this.id = id;
            this.game_id=game_id;
            this.Name = name;
            this.FilePath = filepath;
        }
    }


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

        public EditableLabelEditorViewModel<string> EditableLabelEditor { get; } = new EditableLabelEditorViewModel<string>("defName");

        public ValueNodeInputViewModel<ITypedExpression<string>> Text { get; }


        public ValueNodeOutputViewModel<ITypedExpression<string>> Output { get; }

        private List<NPC> LoadSampleData()
        {
            List<NPC> localNPCs = new List<NPC>();

            localNPCs.Add(new NPC(1, 1, "Mongo", @"Resources\Images\Mongo.jpg"));
            localNPCs.Add(new NPC(2, 3, "Arthur", @"Resources\Images\Arthur.png"));
            localNPCs.Add(new NPC(3, 5, "Farmar", @"Resources\Images\Farmar.png"));
            localNPCs.Add(new NPC(4, 2, "Jerry", @"Resources\Images\Jerry.png"));
            localNPCs.Add(new NPC(5, 4, "Amanda", @"Resources\Images\Amanda.png"));

            return localNPCs;
        }

        private List<NPC> NPCs;

        // currently selected NPC of the node
        private NPC choosenNPC;

        /// <summary>
        /// Sets current NPC according to supplied name
        /// </summary>
        /// <param name="npcName">Name property of the NPC</param>
        void SelectNPC(string npcName)
        {
            try
            {
                var npc = NPCs.First(s => s.Name == npcName);
                var fileStream = new FileStream(npc.FilePath, FileMode.Open);
                HeaderIcon = Splat.BitmapLoader.Current.Load(fileStream, 2 * 163, 2 * 344).Result;
                fileStream.Close();
                choosenNPC = npc;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is FileNotFoundException)
            { 
                
            }
        }

        public QuestNode() : base(NodeType.Literal)
        {
            // collect all NPCs
            NPCs = LoadSampleData();

            // Default NPC
            choosenNPC = NPCs.First(s => s.id == 3);

            this.Name = choosenNPC.Name;

            // HeaderIconButton action
            this.HeaderIconButton = ReactiveCommand.Create(() =>
            {
                var rand = new Random();
                int npcID = 1;
                do
                    npcID = rand.Next(1, NPCs.Count + 1);
                while (npcID == choosenNPC.id); 

                Name = NPCs.First(s => s.id == npcID).Name;
            });

            // Watch Name property for change
            this.WhenAnyValue( x => x.Name ).Subscribe( name => { SelectNPC(name); });

            Text = new CodeGenInputViewModel<ITypedExpression<string>>(PortType.String)
            {
                //Name = "Node name",
                Editor = EditableLabelEditor,//,
                               
                //Value = EditableLabelEditor.ValueChanged.Select(v => new BoolLiteral{ Value = v })
            };
            this.Inputs.Add(Text);


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
