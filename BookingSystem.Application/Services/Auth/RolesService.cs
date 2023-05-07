using Microsoft.AspNetCore.Identity;
using BookingSystem.Application.Error;
using BookingSystem.Domin.Entities;
using BookingSystem.Application.IServices.Auth;

namespace BookingSystem.Application.Application.Services.Auth
{
    public class RolesService : IRolesService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse> AddUserToRoleAsync(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null || !await _roleManager.RoleExistsAsync(roleName))
                return new ApiResponse(401, "invalid user name or role name");

            if (await _userManager.IsInRoleAsync(user, roleName))
                return new ApiResponse(400, "user already assigned to this role");

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

        public async Task<ApiResponse> RemoveUserFromRoleAsync(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null || !await _roleManager.RoleExistsAsync(roleName))
                return new ApiResponse(401, "invalid user name or role name");

            if (!await _userManager.IsInRoleAsync(user, roleName))
                return new ApiResponse(400, "user already not assigned to this role");

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (!result.Succeeded)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }
    }
}
