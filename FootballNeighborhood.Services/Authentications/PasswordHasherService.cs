using System.Text;
using FootballNeighborhood.Domain.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace FootballNeighborhood.Services.Authentications;

public class PasswordHasherService : IPasswordHasherService
{
    private readonly TokenOptions _tokenOptions;

    public PasswordHasherService(
        IOptions<TokenOptions> tokenOptions)
    {
        _tokenOptions = tokenOptions.Value;
    }

    public string GenerateHash(string password)
    {
        var hash = KeyDerivation.Pbkdf2(
            password,
            Encoding.UTF8.GetBytes(_tokenOptions.Salt),
            KeyDerivationPrf.HMACSHA512,
            10000,
            256 / 8);

        return Convert.ToBase64String(hash);
    }

    public bool CompareHash(string passwordHash, string password)
    {
        var target = GenerateHash(password);
        return passwordHash == target;
    }
}