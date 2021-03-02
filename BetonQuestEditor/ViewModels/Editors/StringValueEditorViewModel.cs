using BetonQuestEditorApp.Views.Editors;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace BetonQuestEditorApp.ViewModels.Editors
{
    public class StringValueEditorViewModel : ValueEditorViewModel<string>
    {
        static StringValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new StringValueEditorView(), typeof(IViewFor<StringValueEditorViewModel>));
        }

        public StringValueEditorViewModel()
        {
            Value = "";
        }
    }
}
