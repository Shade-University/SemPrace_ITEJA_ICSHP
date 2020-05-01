using SemPrace_ITEJA_ICSHP;

namespace GUI.ViewModel
{
    internal class ShellTabViewModel : BaseViewModel
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
        } //Binded Canvas size
    }
}
