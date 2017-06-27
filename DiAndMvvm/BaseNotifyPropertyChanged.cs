using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DiAndMvvm
{
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged, IRaisePropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void IRaisePropertyChanged.OnPropertyChanged(string propertyName)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}