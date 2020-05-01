using GUI.Model;
using LanguageLogic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ASTVisualizeTab.xaml
    /// </summary>
    public partial class ASTVisualizeTab : UserControl
    {
        public ASTVisualizeTab(string code)
        {
            InitializeComponent();

            DisplayTree(code);
        }

        private void DisplayTree(string code)
        {
            try
            {
                Parser parser = new Parser(new Lexer(code));
                ASTBuilderTree builderTree = new ASTBuilderTree(parser);

                builderTree.BuildGraph(mainTreeView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parse error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
