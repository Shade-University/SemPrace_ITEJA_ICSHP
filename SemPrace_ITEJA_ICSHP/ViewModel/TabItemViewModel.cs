using SemPrace_ITEJA_ICSHP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GUI.ViewModel
{
    class TabItemViewModel : BaseViewModel
    {
        public string Header { get; set; }
        public UserControl Content { get; set; }

        public bool Closable { get; set; } = true;
    }
}
