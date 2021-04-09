using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetonQuestEditorApp.Views;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using ReactiveUI;

namespace BetonQuestEditorApp.ViewModels
{

    public class NPC : ReactiveObject
    {
        public int Id { get; }
        public int Game_id { get; }
        public string Name { get; }
        public string FilePath { get; }
 
        public NPC(int id, int game_id, string name, string filepath)
        {
            this.Id = id;
            this.Game_id = game_id;
            this.Name = name;
            this.FilePath = filepath;
        }
    }

    /// <summary>
    /// Datagrid for choosing one item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NpcChooseDatagridViewModel : NodeViewModel
    {
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

        #region NPCValue
        private NPC _npcValue;
        public NPC NpcValue
        {
            get => _npcValue;
            set => this.RaiseAndSetIfChanged(ref _npcValue, value);
        }
        #endregion

        // So that we dont need to create new instance every time in 'NpcSelectNew'
        private NpcChooseDatagridView NpcChooseView = new NpcChooseDatagridView();


        //All of the NPCs the editor knows of
        public List<NPC> NPCs { get; private set; }

        static NpcChooseDatagridViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NpcChooseDatagridView(), typeof(IViewFor<NpcChooseDatagridViewModel>));
        }

        public NpcChooseDatagridViewModel()
        {
            // collect all NPCs
            NPCs = LoadSampleData();

            // Default NPC
            NpcValue = NPCs.First(s => s.Id == 1);
        }

        public void NpcSelectNew()
        {
            // If few just click thru
            if (NPCs.Count < 4)
                NpcValue = NPCs.First(s => s.Id == ((NpcValue.Id + 1) % (NPCs.Count)) + 1);
            //otherwise choose from list
            else
            {
                var newNpc = NpcChooseView.ChooseOneNPC(NPCs);
                if( newNpc != null)
                    NpcValue = newNpc;
            }
        }
       
    }
}
