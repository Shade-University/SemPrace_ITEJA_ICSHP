using GUI.Services;
using Microsoft.Win32;
using System;
using System.Windows;

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
