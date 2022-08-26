using Microsoft.EntityFrameworkCore;
using Kontakt.API.Models;

namespace Kontakt.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
