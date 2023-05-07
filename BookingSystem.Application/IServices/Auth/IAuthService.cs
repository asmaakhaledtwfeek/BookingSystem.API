using BookingSystem.Application.Dto;
using BookingSystem.Application.Error;

namespace BookingSystem.Application.IServices.Auth
{
    public interface IAuthService
    {
        public Task<AuthDto> LoginAsync(LoginDto model);

        public Task<AuthDto> RegisterAsync(RegisterDto model);

        public Task<ApplicationUserDto?> GetUserData(string userName);

        public Task<ApiResponse> UpdateUserData(UpdateApplicationUserDto userDto, string userId);

    }
}
