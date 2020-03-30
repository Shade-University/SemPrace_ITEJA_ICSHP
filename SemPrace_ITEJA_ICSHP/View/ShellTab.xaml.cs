using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ShellTab.xaml
    /// </summary>
    public partial class ShellTab : UserControl
    {
        ShellTabViewModel shellTabViewModel;
        public ShellTab()
        {
            InitializeComponent();
            shellTabViewModel = new ShellTabViewModel();
            DataContext = shellTabViewModel;
        }

        private void MyCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            shellTabViewModel.CanvasSizeText = (int)e.NewSize.Width + "x" + (int)e.NewSize.Height;
        }
    }
}
