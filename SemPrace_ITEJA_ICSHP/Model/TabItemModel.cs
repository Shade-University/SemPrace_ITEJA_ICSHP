﻿using SemPrace_ITEJA_ICSHP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GUI.ViewModel
{
    class TabItemModel
    {
        public string Header { get; set; }
        public UserControl Content { get; set; }
        public bool Closable { get; set; } = true;
    } //Firstly i wanted MVVM pattern, but WPF is quite hard and lot of things to understand, so i started but i did not continued
}
