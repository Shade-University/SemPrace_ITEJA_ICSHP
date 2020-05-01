using GUI.Services;
using System.Windows.Controls;

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
