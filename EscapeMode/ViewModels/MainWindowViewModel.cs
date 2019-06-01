using System.ComponentModel;

namespace EscapeMode.ViewModels
{
    class MainWindowViewModel
    {
        public object Item { get; set; } = new Item();
    }

    class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Item()
        {
            Lock = true;
        }

        private bool _Lock;
        public bool Lock
        {
            get => _Lock;
            set
            {
                if (PropertyChanged.RaiseIfSet(() => Lock, ref _Lock, value)) { }
            }
        }
    }
}
