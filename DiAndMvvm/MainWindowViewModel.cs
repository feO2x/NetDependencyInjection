using Light.GuardClauses;
using LightInject;

namespace DiAndMvvm
{
    public sealed class MainWindowViewModel : BaseNotifyPropertyChanged, INavigationService
    {
        private readonly IServiceFactory _container;
        private object _currentView;

        public MainWindowViewModel(IServiceFactory container)
        {
            _container = container.MustNotBeNull();
        }

        public object CurrentView
        {
            get => _currentView;
            set => this.SetIfDifferent(ref _currentView, value.MustNotBeNull());
        }

        public string Title => "My Awesome Contacts";

        public void NavigateToContactsView()
        {
            CurrentView = _container.GetInstance<ContactsMasterDetailView>();
        }

        public void NavigateToMainMenu()
        {
            CurrentView = _container.GetInstance<MainMenuView>();
        }
    }
}