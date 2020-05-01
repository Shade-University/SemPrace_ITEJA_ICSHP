using GUI.Model;
using GUI.Services;
using GUI.View;
using GUI.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        private ShellTab shellTab;
        private MainViewModel mainViewModel = new MainViewModel();
        private string currentFile;

        private void OpenNewTab(string header, UserControl content, bool closable)
        {
            TabItemModel tab = new TabItemModel() { Header = header, Content = content, Closable = closable };
            mainViewModel.TabItems.Add(tab);
            actionTabs.SelectedItem = tab; //Add new tab

        }
        public MainWindow()
        {
            InitializeComponent();
            shellTab = new ShellTab();
            OpenNewTab("Shell", shellTab, false);

            DataContext = mainViewModel;
        }

        private void ShowAST_Click(object sender, RoutedEventArgs e)
        {
            OpenNewTab("AST Visualizer", new ASTVisualizeTab(shellTab.GetHistoryCodeWithEnd()), true);
        }

        private void CloseTab(object sender, MouseButtonEventArgs e)
        {
            mainViewModel.TabItems.RemoveAt(actionTabs.SelectedIndex); //Close Tab
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); //exit application
        }

        private void ShowGeneratedCode_Click(object sender, RoutedEventArgs e)
        {
            string code = shellTab.GetHistoryCodeWithEnd(); //Get code from shellTab

            OpenNewTab("Show code", new GeneratedCodeTab(code), true);
        }

        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            OpenNewTab("New file", new CodeTab(""), true);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file (*.txt)|*.txt|Script file (*.script)|*.script";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                string output = File.ReadAllText(openFileDialog.FileName);
                OpenNewTab(openFileDialog.SafeFileName, new CodeTab(output), true);
            }

        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|Script file (*.script)|*.script";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                string output = RichTextBoxFormatter.GetFormattedCode(shellTab.GetHistoryCodeWithEnd());
                File.WriteAllText(saveFileDialog.FileName, output);
                currentFile = saveFileDialog.FileName;
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFile))
            {
                SaveToFile_Click(null, null);
                return;
            }

            string output = RichTextBoxFormatter.GetFormattedCode(shellTab.GetHistoryCodeWithEnd());
            File.WriteAllText(currentFile, output);

        }

        private void ImgSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                ImgConverter.CreateBitmapFromVisual(shellTab.MyCanvas, saveFileDialog.FileName);
            }
        }
    }
}
