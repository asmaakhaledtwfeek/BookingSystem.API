using BookingSystem.Application.Dto;
using BookingSystem.Application.Error;
using BookingSystem.Application.IServices.Auth;
using BookingSystem.Domin.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace MyHero.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<AuthDto> LoginAsync(LoginDto model)
        {
            var authModel = new AuthDto();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "email or password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await _tokenService.CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.PhoneNumber = user.PhoneNumber;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            return authModel;
        }

        public async Task<AuthDto> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthDto { Message = "email is already exist!" };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthDto { Message = "username is already in use!" };

            if (await _userManager.Users.FirstOrDefaultAsync(p => p.PhoneNumber == model.PhoneNumber) is not null)
                return new AuthDto { Message = "phone number is already exist!" };

            if (!await _roleManager.RoleExistsAsync(model.UserRole))
                return new AuthDto { Message = "invalid role name!" };

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthDto { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, model.UserRole);

            var jwtSecurityToken = await _tokenService.CreateJwtToken(user);


            return new AuthDto
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { model.UserRole },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }

        public async Task<ApplicationUserDto?> GetUserData(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user == null)
                return null;

            var userDto = _mapper.Map<ApplicationUserDto>(user);

            return userDto;
        }

        public async Task<ApiResponse> UpdateUserData(UpdateApplicationUserDto userDto, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new ApiResponse(401);

            if(userDto.UserName != null)
            {
                var isUsedBefor = await _userManager.FindByNameAsync(userDto.UserName);
                if(isUsedBefor != null)
                {
                    return new ApiResponse(400, "username used before");
                }
                else
                {
                    user.UserName = userDto.UserName;
                }
            }

            if (userDto.Email != null)
            {
                var isUsedBefor = await _userManager.FindByEmailAsync(userDto.Email);
                if (isUsedBefor != null)
                {
                    return new ApiResponse(400, "email used before");
                }
                else
                {
                    user.Email = userDto.Email;
                }
            }

            if (userDto.PhoneNumber != null)
            {
                var isUsedBefor = await _userManager.Users.FirstOrDefaultAsync(p => p.PhoneNumber == userDto.PhoneNumber);
                if (isUsedBefor != null)
                {
                    return new ApiResponse(400, "phone number used before");
                }
                else
                {
                    user.PhoneNumber = userDto.PhoneNumber;
                }
            }

            user.FullName = userDto.FullName ?? user.FullName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

    }
}
