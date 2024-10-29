using src.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace src.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(AppUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string? token);
    }
}