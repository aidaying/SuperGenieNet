using Genie.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Genie.Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAysnc(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Bob",
                Email = "bob@test.com",
                UserName = "bob@test.com",
                Address = new Address
                {
                    FirstName = "Bob",
                    LastName = "Bobity",
                    Street = "10 Test Street",
                    City = "Auckland",
                    Postcode = "1010"
                }
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}

