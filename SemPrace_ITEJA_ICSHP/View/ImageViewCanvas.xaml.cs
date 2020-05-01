using GUI.Services;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ImageViewCanvas.xaml
    /// </summary>
    public partial class ImageViewCanvas : Window
    {
        public ImageViewCanvas()
        {
            InitializeComponent();
        }

        private void Img_save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (dialog.ShowDialog() == true)
            {
                ImgConverter.CreateBitmapFromVisual(ImgCanvas, dialog.FileName);
            }
        }
    }
}
