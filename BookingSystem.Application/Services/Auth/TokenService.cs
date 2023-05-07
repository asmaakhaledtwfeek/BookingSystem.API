using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookingSystem.Domin.Entities;
using BookingSystem.Application.IServices.Auth;

namespace BookingSystem.Application.Application.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id!),
                new Claim(ClaimTypes.Name, user.FullName!),
                new Claim(ClaimTypes.NameIdentifier, user.UserName!),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!),
                new Claim(ClaimTypes.Email, user.Email!),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudiance"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:DurationInMinutes"]!)),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
