using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database;

public class DbInitializer
{
    public async static void Initialize(TwitterDbContext context)
    {
        var ukraine = await context.Countries.FirstOrDefaultAsync(c => c.Name == "Ukraine");
        var kyiv = await context.Cities.FirstOrDefaultAsync(c => c.Name == "Kyiv");

        if (ukraine == null || kyiv == null)
        {
            // Можна створити, якщо ще не існують
            ukraine = new Country { Id = Guid.NewGuid(), Name = "Ukraine", PhoneCode = "+380" };
            kyiv = new City { Id = Guid.NewGuid(), Name = "Kyiv" };

            await context.Countries.AddRangeAsync(ukraine);
            await context.Cities.AddRangeAsync(kyiv);
            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            var user = new User
            {
                Gmail = "admin@admin.com",
                Password = "adminadmin",
                Nickname = "admin",
                Name = "Andrew",
                Surname = "Manin",
                PhoneNumber = "+3800000000",
                Status = "admin",
                Description = "System administrator",
                Created = DateTime.UtcNow,
                CountryId = ukraine.Id,
                CityId = kyiv.Id,
            };

            await context.Users.AddRangeAsync(user);
            await context.SaveChangesAsync();
        }
    }
    public async Task<List<User>> GetUserByEmailAsync(TwitterDbContext context)
    {
        var user = await context.Users.ToListAsync();

        return user;
    }
}