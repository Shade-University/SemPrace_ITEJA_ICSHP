using System.Windows.Controls;

namespace GUI.ViewModel
{
    internal class TabItemModel
    {
        public string Header { get; set; }
        public UserControl Content { get; set; }
        public bool Closable { get; set; } = true;
    } //Firstly i wanted MVVM pattern, but WPF is quite hard and lot of things to understand, so i started but i did not continued
}
