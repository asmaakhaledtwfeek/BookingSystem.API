using BookingSystem.Application.Error;

namespace BookingSystem.Application.IServices.Auth
{
    public interface IRolesService
    {
        public Task<ApiResponse> AddUserToRoleAsync(string userName, string roleName);
        public Task<ApiResponse> RemoveUserFromRoleAsync(string userName, string roleName);
    }
}
