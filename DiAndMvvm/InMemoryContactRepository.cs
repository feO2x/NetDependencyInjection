using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DiAndMvvm
{
    public sealed class InMemoryContactRepository : IContactRepository
    {
        private readonly ObservableCollection<Contact> _contacts =
            new ObservableCollection<Contact>
            {
                new Contact { FirstName = "Robert C.", LastName = "Martin", BirthDate = new DateTime(1952, 5, 3), Email = "uncle@bob.com" },
                new Contact { FirstName = "Mark", LastName = "Seemann", Email = "info@ploeh.dk" },
                new Contact { FirstName = "Martin", LastName = "Fowler", BirthDate = new DateTime(1963, 7, 26), Phone = "44145 - 1122345" }
            };

        public Task<ObservableCollection<Contact>> LoadContactsAsync()
        {
            return Task.FromResult(_contacts);
        }
    }
}