using GUI.Model;
using GUI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for GeneratedCodeTab.xaml
    /// </summary>
    public partial class GeneratedCodeTab : UserControl
    {
        public GeneratedCodeTab(string code)
        {
            InitializeComponent();
            RichTextBoxFormatter.LoadRichTextBox(richTxtBox_GeneratedCode, code);
            RichTextBoxFormatter.FormatCode(richTxtBox_GeneratedCode); //Load and format to richtextbox
        }
    }
}
