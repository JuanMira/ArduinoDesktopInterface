using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;


namespace MaterialUI.Core
{
    internal class ObservableObject : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {                        
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
