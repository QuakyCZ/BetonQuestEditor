using BetonQuestEditorApp.Views.Editors;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace BetonQuestEditorApp.ViewModels.Editors
{
    public class IntegerValueEditorViewModel : ValueEditorViewModel<int?>
    {
        static IntegerValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new IntegerValueEditorView(), typeof(IViewFor<IntegerValueEditorViewModel>));
        }

        public IntegerValueEditorViewModel()
        {
            Value = 0;
        }
    }
}
