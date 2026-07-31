using ECommerce.Application.Contacts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Infrastructure.Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings settings;

        public TokenService(IOptions<JwtSettings> jwtOptions)
        {
            settings = jwtOptions.Value;
        }

        public string CreateToken(string userId, string email, string userName, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Name, userName)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            if (string.IsNullOrWhiteSpace(settings.SecretKey))
                throw new InvalidOperationException("JWT SecretKey is missing");

            if (settings.SecretKey.Length < 32)
                throw new InvalidOperationException("JWT SecretKey is too short (must be at least 32 characters)");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(settings.ExpirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class JwtSettings
    {
        public string SecretKey { get; init; } = default!;
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public int ExpirationMinutes { get; init; } = 60;
    }
}
