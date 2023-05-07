using Microsoft.AspNetCore.Identity;
using BookingSystem.Application.Application.Dto;
using BookingSystem.Application.Error;
using BookingSystem.Application.Dto;
using BookingSystem.Application.Helper.Email;
using BookingSystem.Domin.Entities;
using BookingSystem.Application.IServices.Auth;

namespace BookingSystem.Application.Application.Services.Auth
{
    public class PasswordService : IPasswordService
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public PasswordService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ApiResponse> SendOTPAsync(SendOTPDto sendOTPDto)
        {
            var user = await _userManager.FindByEmailAsync(sendOTPDto.Email);

            if (user == null)
                return new ApiResponse(401);

            string otp = new Random().Next(1000, 9999).ToString();

            user.OTP = otp;
            user.OTPExpirationDate = DateTime.UtcNow.AddMinutes(5);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new ApiResponse(400);

            MailSettings.Send(user.Email, "Booking System Verify Password", $"Hello  Dear {user.FullName}\nthis is your reset password OTP {user.OTP}\nvalid unitl {user.OTPExpirationDate}");

            return new ApiResponse(200);
        }

        public async Task<ApiResponse> VerifyOTPAsync(VerfiyOTPDto verfiyOTPDto)
        {
            var user = await _userManager.FindByEmailAsync(verfiyOTPDto.Email);

            if (user == null)
                return new ApiResponse(401);

            if (DateTime.UtcNow.CompareTo(user.OTPExpirationDate) > 0)
                return new ApiResponse(400, "OTP is expired");

            if (user.OTP != verfiyOTPDto.OTP)
                return new ApiResponse(400, "incorect OTP");

            return new ApiResponse(200);
        }

        public async Task<ApiResponse> NewPasswordAsync(SetNewPasswordDto setNewPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(setNewPasswordDto.Email);

            if (user == null)
                return new ApiResponse(401);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, setNewPasswordDto.Password);

            if (!result.Succeeded)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

        public async Task<ApiResponse> ChangePasswordAsync(ChangePasswordDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
                return new ApiResponse(401);

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }
    }
}
