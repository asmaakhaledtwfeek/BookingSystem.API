using Microsoft.AspNetCore.Identity;

namespace BookingSystem.API.Extensions
{
    public static class ApplicationRolesServiceExtension
    {
        public async static Task<IServiceCollection> AddRolesAsync(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roleNames = new List<string> { "user", "admin" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }

            return services;
        }
    }
}
