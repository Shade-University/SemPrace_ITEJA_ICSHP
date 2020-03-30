using SemPrace_ITEJA_ICSHP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GUI.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public ObservableCollection<TabItemViewModel> TabItems { get; } = new ObservableCollection<TabItemViewModel>();

    }
}
