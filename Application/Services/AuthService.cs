using API.Domain.Entities;
using API.Infra.Http.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Application.Services;

public class AuthService(IOptionsSnapshot<AuthorizationConfig> authConfig)
{
    private readonly AuthorizationConfig _authConfig = authConfig.Value;

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            //new Claim(ClaimTypes.Role, user.Role),
            new("permission", "update_user")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
           issuer: _authConfig.Issuer,
           audience: _authConfig.Audience,
           claims: claims,
           expires: DateTime.UtcNow.AddHours(2),
           signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
