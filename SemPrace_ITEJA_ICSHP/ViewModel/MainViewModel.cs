using SemPrace_ITEJA_ICSHP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GUI.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public ObservableCollection<TabItemModel> TabItems { get; } = new ObservableCollection<TabItemModel>();
        //Open-close tab collection. Binded
    }
}
