using BookingSystem.Application.Application.Dto;
using BookingSystem.Application.Application.Services.Auth;
using BookingSystem.Application.Dto;
using BookingSystem.Application.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyHero.Application.Services;
using System.Security.Claims;

namespace BookingSystem.API.Controllers
{
    [Authorize]
    public class AuthController : BaseController
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;
        private readonly PasswordService _passwordService;

        public AuthController(AuthService authService, TokenService tokenService,
            PasswordService passwordService)
        {
            _authService = authService;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<AuthDto>> Login([FromBody] LoginDto loginDto)
        {
            AuthDto authDto = await _authService.LoginAsync(loginDto);

            if (!authDto.IsAuthenticated)
                return BadRequest(authDto);

            return Ok(authDto);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<AuthDto>> Register([FromBody] RegisterDto registerDto)
        {
            AuthDto authDto = await _authService.RegisterAsync(registerDto);

            if (!authDto.IsAuthenticated)
                return BadRequest(authDto);

            return Ok(authDto);
        }

        [AllowAnonymous]
        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
            => Ok(await _passwordService.ChangePasswordAsync(changePasswordDto));

       
        [AllowAnonymous]
        [HttpGet("GetUserData")]
        public async Task<ActionResult<ApplicationUserDto>> GetUserData(string? userName = null)
        {
            if (userName == null)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
                {
                    userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                else
                {
                    return NotFound(new ApiResponse(404, "user not found"));
                }
            }


            var userDto = await _authService.GetUserData(userName);

            if (userDto == null)
                return NotFound(new ApiResponse(404, "user not found"));

            return Ok(userDto);
        }

        [HttpPut("UpdateUserData")]
        public async Task<ActionResult> UpdateUserData([FromBody] UpdateApplicationUserDto userDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.Sid);

            var result = await _authService.UpdateUserData(userDto, userId);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("SendOTP")]
        public async Task<ActionResult> SendOTP(SendOTPDto sendOTPDto)
            => Ok(await _passwordService.SendOTPAsync(sendOTPDto));

        [AllowAnonymous]
        [HttpPost("VerifyOTP")]
        public async Task<ActionResult> VerifyOTP(VerfiyOTPDto verfiyOTPDto)
           => Ok(await _passwordService.VerifyOTPAsync(verfiyOTPDto));

        [AllowAnonymous]
        [HttpPost("SetNewPassword")]
        public async Task<ActionResult> NewPassword(SetNewPasswordDto setNewPasswordDto)
            => Ok(await _passwordService.NewPasswordAsync(setNewPasswordDto));
    }
}
