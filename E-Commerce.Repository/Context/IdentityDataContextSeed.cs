using E_Commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Repository.Context
{
    public static class IdentityDataContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "AllaaFarrag",
                    Email = "AllaaFarrag15@gmail.com",
                    DisplayName = "Allaa Farrag",
                    Address = new Address
                    {
                        City = "Cairo",
                        Country = "Egypt",
                        PostalCode = "12345",
                        State = "Za",
                        Street = "12345",
                    }
                };

                await userManager.CreateAsync(user , "P@ssw0rd");
            }
        }
    }
}
