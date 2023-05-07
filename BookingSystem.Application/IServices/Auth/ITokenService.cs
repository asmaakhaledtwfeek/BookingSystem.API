using BookingSystem.Domin.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace BookingSystem.Application.IServices.Auth
{
    public interface ITokenService
    {
        public Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
    }
}
