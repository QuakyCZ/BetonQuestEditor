﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetonQuestEditorApp.Views;
using NodeNetwork.ViewModels;
using ReactiveUI;

namespace BetonQuestEditorApp.ViewModels
{
    public class CodeGenPendingConnectionViewModel : PendingConnectionViewModel
    {
        static CodeGenPendingConnectionViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new CodeGenPendingConnectionView(), typeof(IViewFor<CodeGenPendingConnectionViewModel>));
        }

        public CodeGenPendingConnectionViewModel(NetworkViewModel parent) : base(parent)
        {

        }
    }
}
