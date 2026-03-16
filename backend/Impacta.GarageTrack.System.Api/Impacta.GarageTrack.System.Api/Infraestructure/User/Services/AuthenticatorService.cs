using Impacta.GarageTrack.System.Api.Application.User.Services;
using Impacta.GarageTrack.System.Api.Domain.Kernel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Impacta.GarageTrack.System.Api.Infraestructure.User.Services
{
    public class AuthenticatorService : IAuthenticatorService
    {
        private readonly JwtSettings _jwtSettings;

        public AuthenticatorService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(UserSession userSession)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userSession.UserId.ToString()),
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.GivenName, userSession.UserName),
                new Claim(ClaimTypes.Surname, userSession.UserName),
                new Claim(ClaimTypes.Role, userSession.UserRole.ToString()),
                new Claim("CompanyName", userSession.CompanyName.ToString()),
                new Claim("CompanyId", userSession.CompanyId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpirationInMinutes { get; set; } = 60;
    }
}
