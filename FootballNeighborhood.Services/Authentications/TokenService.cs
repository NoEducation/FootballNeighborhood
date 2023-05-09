using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FootballNeighborhood.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FootballNeighborhood.Services.Authentications;

public class TokenService : ITokenService
{
    private readonly TokenOptions _tokenOptions;

    public TokenService(IOptions<TokenOptions> tokenOptions)
    {
        _tokenOptions = tokenOptions.Value;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secrete = Encoding.ASCII.GetBytes(_tokenOptions.Secrete);
        var jwtHandler = new JwtSecurityTokenHandler();
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenTimeValid),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secrete),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = jwtHandler.CreateToken(descriptor);
        return jwtHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(40);
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        throw new NotImplementedException();
    }
}