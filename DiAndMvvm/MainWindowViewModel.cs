using Light.ChangeNotifications;
using Light.GuardClauses;

namespace DiAndMvvm
{
    public sealed class MainWindowViewModel : BaseNotifyPropertyChanged
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set => this.SetIfDifferent(ref _currentView, value.MustNotBeNull());
        }

        public string Title => "My Awesome Contacts";
    }
}