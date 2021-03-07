using BetonQuestEditorApp.Views.Editors;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace BetonQuestEditorApp.ViewModels.Editors
{
    /// <summary>
    /// Model for the ButtonEditorView, the button primitive control
    /// </summary>
    public class BoolValueEditorViewModel : ValueEditorViewModel<bool>
    {
        static BoolValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new ButtonEditorView(), typeof(IViewFor<BoolValueEditorViewModel>));
        }

        public BoolValueEditorViewModel()
        {
            // Intital value (?)
            Value = false;
        }
    }
}
