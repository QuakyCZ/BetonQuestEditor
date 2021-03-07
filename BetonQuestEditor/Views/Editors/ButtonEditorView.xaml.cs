using System.Windows;
using BetonQuestEditorApp.ViewModels;
using BetonQuestEditorApp.ViewModels.Editors;
using ReactiveUI;

namespace BetonQuestEditorApp.Views.Editors
{
    /// <summary>
    /// View for the primitive button control
    /// </summary>
    public partial class ButtonEditorView : IViewFor<BoolValueEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(BoolValueEditorViewModel), typeof(ButtonEditorView), new PropertyMetadata(null));

        public BoolValueEditorViewModel ViewModel
        {
            get => (BoolValueEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (BoolValueEditorViewModel)value;
        }
        #endregion

        /// <summary>
        /// Bindind of the model to the button press
        /// </summary>
        public ButtonEditorView()
        {
            InitializeComponent();

            this.WhenActivated(d => d(
                this.Bind(ViewModel, vm => vm.Value, v => v.Button.IsPressed)
            ));
        }
    }
}
