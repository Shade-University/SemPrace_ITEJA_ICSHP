using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace GUI.Services
{
    public static class RichTextBoxFormatter
    {
        public static void LoadRichTextBox(RichTextBox richTextBox, string code)
        {
            using (StringReader reader = new StringReader(code))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Paragraph paragraph = new Paragraph();
                    paragraph.LineHeight = 1;
                    paragraph.Inlines.Add(new Run(line));

                    richTextBox.Document.Blocks.Add(paragraph);
                }
            }
        }
        public static void FormatCode(RichTextBox richTextBox)
        {
            List<TextRange> wordRanges = GetAllWordRanges(richTextBox.Document);
            foreach (TextRange wordRange in wordRanges)
            {
                switch (wordRange.Text)
                {
                    case "var":
                        wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGoldenrod);
                        break;
                    case "func":
                        wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.OliveDrab);
                        break;
                    case "if":
                    case "then":
                    case "while":
                    case "do":
                    case "for":
                    case "to":
                        wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.RoyalBlue);
                        break;
                    case "write":
                    case "pen":
                    case "angle":
                    case "backward":
                    case "forward":
                        wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.MediumPurple);
                        break;
                    case "}":
                        wordRange.Text = "\n" + wordRange.Text;
                        break;
                    case "{":
                        wordRange.Text = "\n" + wordRange.Text + "\n";
                        break;
                    default:
                        break;
                }
            }
        }
        public static string GetFormattedCode(string code)
        {
            RichTextBox rTxtBox = new RichTextBox(new FlowDocument());
            LoadRichTextBox(rTxtBox, code);
            FormatCode(rTxtBox);

            return new TextRange(rTxtBox.Document.ContentStart, rTxtBox.Document.ContentEnd).Text;
        }
        private static List<TextRange> GetAllWordRanges(FlowDocument document)
        {
            List<TextRange> list = new List<TextRange>();
            string pattern = @"(\w+)|{|}"; //word regex or { }
            TextPointer pointer = document.ContentStart;

            while (pointer != null) //run via whole text
            {

                string textRun = pointer.GetTextInRun(LogicalDirection.Forward); //Get text
                MatchCollection matches = Regex.Matches(textRun, pattern); //Try to match word
                foreach (Match match in matches)
                {
                    int startIndex = match.Index;
                    int length = match.Length;
                    TextPointer start = pointer.GetPositionAtOffset(startIndex);
                    TextPointer end = start.GetPositionAtOffset(length); //Get matches positions
                    list.Add(new TextRange(start, end));
                }
                pointer = pointer.GetNextContextPosition(LogicalDirection.Forward); //Move forward

            }

            return list;

        }
    }
}
