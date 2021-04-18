using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BetonQuestEditorApp.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;

namespace BetonQuestEditorApp.Views
{
    public partial class NpcChooseDatagridView : IViewFor<NpcChooseDatagridViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(NpcChooseDatagridViewModel), typeof(NpcChooseDatagridView), new PropertyMetadata(null));

        public NpcChooseDatagridViewModel ViewModel
        {
            get => (NpcChooseDatagridViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (NpcChooseDatagridViewModel)value;
        }
        #endregion

        public NpcChooseDatagridView()
        {
            InitializeComponent();
        }

        private static Window container { get; set; }  // Property, because we need it in different places

        private static int _npcValue;  //Property, because we need it in different places
        public static int NpcValue { get { return _npcValue; } set { _npcValue = value; } }

        /// <summary>
        /// Window_Loaded handler
        /// Postitions the window according to the mouse pointer position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Window).Top = Mouse.GetPosition(null).Y + 5;
            (sender as Window).Left = Mouse.GetPosition(null).X + 5;
        }

        /// <summary>
        /// Selects new NPC using datagrid
        /// </summary>
        /// <param name="NPCs">List of all the NPCs</param>
        /// <returns></returns>
        public NPC ChooseOneNPC(List<NPC> NPCs)
        {
            var contentControl = new NpcChooseDatagridView();
            // Assign data to the datagrid
            PoulateDataGrid(NPCs, ref contentControl.dataGrid);
            //contentControl.dataGrid.ItemsSource = NPCs;

            // Create something to present
            container = new Window() 
            {
                SizeToContent = SizeToContent.WidthAndHeight,
                Content = contentControl,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Owner = Application.Current.MainWindow,
                WindowStyle = WindowStyle.ToolWindow
            };

            container.Loaded += new RoutedEventHandler(Window_Loaded);

            // Add Event handler to the datagrid
            Style rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new EventSetter(DataGridRow.MouseDoubleClickEvent,
                                     new MouseButtonEventHandler(Row_DoubleClick)));
            contentControl.dataGrid.RowStyle = rowStyle;

            // dssplay datagrid
            container.ShowDialog();

            // handle result
            if (NPCs.FindIndex(x => x.Game_id == NpcValue) == -1)
                return null;
            else
                return NPCs.First(x => x.Game_id == NpcValue);
        }

        /// <summary>
        /// Struct to be used with the datagrid
        /// </summary>
        private struct DataGridNPC
        {
            public int Id { get; set; }
            public string Name { get; set; }
            //public Bitmap NpcImage;
        }

        /// <summary>
        /// Populate datagrid with values
        /// </summary>
        /// <param name="nPCs">List of all the NPCs</param>
        /// <param name="dataGrid">Datagrid to be populated</param>
        private void PoulateDataGrid(List<NPC> nPCs, ref DataGrid dataGrid)
        {
            // create a list for the datagrid
            var dgnl = new List<DataGridNPC>();

            // add each NPC to the new list
            foreach( NPC npc in nPCs)
            {
                dgnl.Add(new DataGridNPC { Id = npc.Game_id, Name = npc.Name });
            }

            // couple datagrid and NPC data
            dataGrid.ItemsSource = dgnl;
            
            // Size the columns according to the width of the data
            dataGrid.ColumnWidth = DataGridLength.SizeToCells;
        }


        //  
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            if (container != null)
            {
                NpcValue = ((DataGridNPC)(row.Item)).Id;
                container.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                DataGridNPC row = (DataGridNPC)dataGrid.SelectedItems[0];

                if (container != null)
                {
                    NpcValue = row.Id;
                    container.Close();
                }
            }
        }

    }
}
