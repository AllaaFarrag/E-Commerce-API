using E_Commerce.Core.Entities.Identity;
using E_Commerce.Repository.Context;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.API.Extentions
{
    public static class DbInitializer
    {
        public static async Task InitializeDbAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = service.GetRequiredService<DataContext>();
                    var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
                    //Create DB if not exist

                    //if ((await context.Database.GetPendingMigrationsAsync()).Any())
                    //    await context.Database.MigrateAsync();

                    //Apply Seeding
                    await DataContextSeed.SeedData(context);
                    await IdentityDataContextSeed.SeedUsersAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                } 
            }
        }
    }
}
