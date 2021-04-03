using System.Globalization;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BetonQuestEditorApp.ViewModels.Nodes;
using ReactiveUI;

namespace BetonQuestEditorApp.Views.Editors
{
    public partial class EditableLabelEditorView : IViewFor<IEditableLabelEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(IEditableLabelEditorViewModel), typeof(EditableLabelEditorView), new PropertyMetadata(null));

        public IEditableLabelEditorViewModel ViewModel
        {
            get => (IEditableLabelEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (IEditableLabelEditorViewModel)value;
        }

        #endregion
        private string _nodeName;
        public string NodeName { get { return RemoveDiacritics(_nodeName); } set { _nodeName = value; } }

        public EditableLabelEditorView()
        {
            InitializeComponent();

            NodeName = "";
            nameLabel.Visibility = Visibility.Visible;
            nameTextBox.Visibility = Visibility.Hidden;

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.Value, v => v.nameTextBox.Text).DisposeWith(d);
            });


            this.WhenActivated(d =>
            {
                //this.Bind(ViewModel, vm => vm.Endpoint.Name, v => v.nameTextBox.Text).DisposeWith(d);
                this.Bind(ViewModel,
                    vm => vm.Endpoint.Name,
                    v => v.NodeName)
                .DisposeWith(d);
            });
        }
        //  v => v.NodeName)
        //  v => v.nameTextBox.Text)

        /// <summary>
        /// Remove Diacritics function.
        /// Use underscore for whitespaces.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    if (unicodeCategory == UnicodeCategory.SpaceSeparator)
                        stringBuilder.Append('_');
                    else
                        stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Handler for the mouse double click
        /// Hides the Label and shows the TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                nameLabel.Visibility = Visibility.Hidden;
                nameTextBox.Visibility = Visibility.Visible;
                nameTextBox.Focus();
                nameTextBox.SelectAll();
            }
        }

        /// <summary>
        /// Handler for the key press 
        /// Currently only handles Return key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == Key.Return)
                {
                    _nodeName = nameTextBox.Text;
                    nameTextBox.Visibility = Visibility.Hidden;
                    nameLabel.Visibility = Visibility.Visible;
                    textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
        }
    }
}
