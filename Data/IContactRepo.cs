using Kontakt.API.Models;

namespace Kontakt.API.Data
{
    public interface IContactRepo
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();

        Task<Contact> GetContactByIdAsync(int id);

        Task CreateContactAsync(Contact contact);

        Task<Contact> UpdateContactAsync(Contact contact);

        Task DeleteContactAsync(Contact contact);

        Task<bool> SaveChangesAsync();
    }
}
