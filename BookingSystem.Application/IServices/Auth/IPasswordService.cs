using BookingSystem.Application.Application.Dto;
using BookingSystem.Application.Dto;
using BookingSystem.Application.Error;

namespace BookingSystem.Application.IServices.Auth
{
    public interface IPasswordService
    {
        public Task<ApiResponse> SendOTPAsync(SendOTPDto sendOTPDto);
        public Task<ApiResponse> VerifyOTPAsync(VerfiyOTPDto verfiyOTPDto);
        public Task<ApiResponse> NewPasswordAsync(SetNewPasswordDto setNewPasswordDto);
        public Task<ApiResponse> ChangePasswordAsync(ChangePasswordDto model);
    }
}
