using Kontakt.API.Models;

namespace Kontakt.API.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext dbContext)
        {
            if (!dbContext.Contacts.Any())
            {
                Console.WriteLine("Seeding data");

                dbContext.Contacts.AddRange(
                    new Contact() { Name = "Robin Seidel", ContactNumber = "+40 1234", Note= "bfalsfds" }
                    );

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}
