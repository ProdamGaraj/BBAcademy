using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Models.Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Auth;

public class AuthoriserService
{
    private JwtConfig _config;

    public AuthoriserService(IOptions<JwtConfig> options)
    {
        _config = options.Value;
    }

    public async Task<string> GenerateToken(long userId, string username)
    {
        var issuer = _config.Issuer;
        var audience = _config.Audience;

        // Now its ime to define the jwt token which will be responsible of creating our tokens
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        // We get our secret from the appsettings
        var key = Encoding.ASCII.GetBytes(_config.Key);
        
        var jwt = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            notBefore: null,
            claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Email, username),
                // the JTI is used for our refresh token which we will be convering in the next video
                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid()
                        .ToString()
                )
            },
            expires: DateTime.UtcNow.AddHours(6),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return encodedJwt;
    }
}