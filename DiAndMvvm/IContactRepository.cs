using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DiAndMvvm
{
    public interface IContactRepository
    {
        Task<ObservableCollection<Contact>> LoadContactsAsync();
    }
}