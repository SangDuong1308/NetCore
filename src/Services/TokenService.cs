using src.Interfaces;
using src.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace src.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
        }
        public async Task<JwtSecurityToken> CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Email, user.Email),
                //new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Get user roles from UserManager
            var roles = await _userManager.GetRolesAsync(user); // Fetch roles asynchronously
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.Now.AddSeconds(30),
            //    SigningCredentials = creds,
            //    Issuer = _config["JWT:Issuer"],
            //    Audience = _config["JWT:Audience"]
            //};

            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(3),
                signingCredentials: creds,
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"]
            );

            //var tokenHandler = new JwtSecurityTokenHandler();

            //var token = tokenHandler.CreateToken(tokenDescriptor);

            //return tokenHandler.WriteToken(token);

            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
        {
            var validation = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _config["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["JWT:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["JWT:Secret"])),
                ValidateLifetime = false, // validate token expiry
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }
    }
}
