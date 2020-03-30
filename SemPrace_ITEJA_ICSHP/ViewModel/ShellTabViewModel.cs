using SemPrace_ITEJA_ICSHP;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.ViewModel
{
    class ShellTabViewModel : BaseViewModel
    {
        private string canvasSizeText;
        public string CanvasSizeText
        {
            get { return canvasSizeText; }
            set
            {
                canvasSizeText = value;
                RaisePropertyChanged("CanvasSizeText");
            }
        }
    }
}
