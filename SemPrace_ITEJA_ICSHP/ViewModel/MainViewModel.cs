using SemPrace_ITEJA_ICSHP;
using System.Collections.ObjectModel;

namespace GUI.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<TabItemModel> TabItems { get; } = new ObservableCollection<TabItemModel>();
        //Open-close tab collection. Binded
    }
}
