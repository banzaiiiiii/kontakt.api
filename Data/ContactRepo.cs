using Microsoft.EntityFrameworkCore;
using Kontakt.API.Models;

namespace Kontakt.API.Data
{
    public class ContactRepo : IContactRepo
    {
        private readonly AppDbContext _dbContext;

        public ContactRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateContactAsync(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(Contact contact)
        {
            _dbContext.Contacts.Remove(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _dbContext.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _dbContext.Contacts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
