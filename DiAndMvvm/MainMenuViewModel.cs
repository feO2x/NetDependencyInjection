using System.Windows.Input;
using Light.GuardClauses;

namespace DiAndMvvm
{
    public sealed class MainMenuViewModel : BaseNotifyPropertyChanged
    {
        private readonly DelegateCommand _navigateToContactsCommand;

        public MainMenuViewModel(INavigationService navigationService)
        {
            navigationService.MustNotBeNull();
            _navigateToContactsCommand = new DelegateCommand(navigationService.NavigateToContactsView);
        }

        public ICommand NavigateToContactsCommand => _navigateToContactsCommand;
    }
}