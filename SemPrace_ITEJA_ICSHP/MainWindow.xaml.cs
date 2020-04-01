using GUI.View;
using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SemPrace_ITEJA_ICSHP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            mainViewModel.TabItems.Add(new TabItemViewModel() { Header = "Shell", Content = new ShellTab(), Closable = false });
            DataContext = mainViewModel;
            actionTabs.SelectedIndex = 0;
        }

        private void ShowAST_Click(object sender, RoutedEventArgs e)
        {
            TabItemViewModel item = new TabItemViewModel() { Header = "AST Visualizer", Content = new ASTVisualizeTab() };
            mainViewModel.TabItems.Add(item);
            actionTabs.SelectedItem = item;
        }

        private void CloseTab(object sender, MouseButtonEventArgs e)
        {
            mainViewModel.TabItems.RemoveAt(actionTabs.SelectedIndex);
        }
    }
}
