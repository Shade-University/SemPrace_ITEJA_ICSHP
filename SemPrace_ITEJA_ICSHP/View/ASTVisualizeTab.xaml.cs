using GUI.Model;
using LanguageLogic;
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
    /// Interaction logic for ASTVisualizeTab.xaml
    /// </summary>
    public partial class ASTVisualizeTab : UserControl
    {
        public ASTVisualizeTab()
        {
            InitializeComponent();
            string my_lang = @"
            x = 10 - 5;
            y = 0;

                    END

             ";
            Lexer lex = new Lexer(my_lang);
            Parser pars = new Parser(lex);
            ASTBuilderTree builder = new ASTBuilderTree(pars);

            builder.BuildGraph(mainTreeView);
        }
    }
}
