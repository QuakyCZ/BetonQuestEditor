using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using DynamicData;
using BetonQuestEditorApp.Views;
using BetonQuestEditorApp.Views.Editors;
using MoreLinq.Extensions;
using NodeNetwork.Toolkit.Group;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using ReactiveUI;
using BetonQuestEditorApp.Model.Compiler;

namespace BetonQuestEditorApp.ViewModels.Nodes
{
    /// <summary>
    /// A non-generic interface that provides access to the data in EditableLabelEditorView.
    /// Mapping a view onto a generic viewmodel is problematic because the generic type often isn't known in the view,
    /// and generic views are often not allowed.
    /// </summary>
    public interface IEditableLabelEditorViewModel    {
        public Endpoint Endpoint { get; }

        public string Value { get; set; }

    }

    public class EditableLabelEditorViewModel<T> : ValueEditorViewModel<ITypedExpression<T>>, IEditableLabelEditorViewModel
    {
        public Endpoint Endpoint => Parent;

        string IEditableLabelEditorViewModel.Value { get => Value; set => Value = value; }

        public string Value = "";
        
        static EditableLabelEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new EditableLabelEditorView(), typeof(IViewFor<EditableLabelEditorViewModel<T>>));
        }

        public EditableLabelEditorViewModel(string nodeName)
        {
            
        }
    }
}
