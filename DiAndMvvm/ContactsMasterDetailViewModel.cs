using System.Collections.ObjectModel;
using System.Windows.Input;
using Light.GuardClauses;

namespace DiAndMvvm
{
    public sealed class ContactsMasterDetailViewModel : BaseNotifyPropertyChanged
    {
        private readonly IContactRepository _contactRepository;
        private readonly DelegateCommand _navigateBackCommand;

        private ObservableCollection<Contact> _contacts;

        private Contact _selectedContact;

        public ContactsMasterDetailViewModel(INavigationService navigationService, IContactRepository contactRepository)
        {
            _navigateBackCommand = new DelegateCommand(navigationService.NavigateToMainMenu);
            _contactRepository = contactRepository.MustNotBeNull();

            LoadContactsAsync();
        }

        public ICommand NavigateBackCommand => _navigateBackCommand;

        public string Title => "Your contacts";

        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set => this.SetIfDifferent(ref _contacts, value.MustNotBeNull());
        }

        public Contact SelectedContact
        {
            get => _selectedContact;
            set => this.SetIfDifferent(ref _selectedContact, value);
        }

        private async void LoadContactsAsync()
        {
            Contacts = await _contactRepository.LoadContactsAsync();
            if (Contacts.Count != 0)
                SelectedContact = Contacts[0];
        }
    }
}