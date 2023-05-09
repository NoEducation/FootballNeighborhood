using System.Security.Claims;
using FootballNeighborhood.Domain.Dtos.Authentications;
using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Services.Authentications;

public class LoginService : ILoginService
{
    private readonly Context _context;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly ITokenService _tokenService;

    public LoginService(Context context,
        IPasswordHasherService passwordHasherService,
        ITokenService tokenService)
    {
        _context = context;
        _passwordHasherService = passwordHasherService;
        _tokenService = tokenService;
    }

    public async Task<OperationResult<UserLoggedDto>> Login(UserCredentialsDto credentials,
        CancellationToken cancellationToken)
    {
        var result = await Validate(credentials, cancellationToken);

        if (!result.Success) return result;

        var claims = await GetClaims(credentials.Login, cancellationToken);

        var token = _tokenService.GenerateAccessToken(claims);

        result.Result = new UserLoggedDto(token);

        return result;
    }

    private async Task<OperationResult<UserLoggedDto>> Validate(UserCredentialsDto credentials,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserLoggedDto>();

        var user = await _context.Users
            .SingleOrDefaultAsync(user => user.Login == credentials.Login, cancellationToken);

        if (user is null)
        {
            result.AddError(AuthenticationsRescource.UserCredentialsInvalid_ErrorMessage);
            return result;
        }

        var password = _passwordHasherService.GenerateHash(credentials.Password);

        if (password != user.Password) result.AddError(AuthenticationsRescource.UserCredentialsInvalid_ErrorMessage);

        return result;
    }

    private async Task<IEnumerable<Claim>> GetClaims(string login, CancellationToken cancellationToken)
    {
        var result = new List<Claim>();

        var user = await _context.Users
            .SingleAsync(user => user.Login == login, cancellationToken);

        var nameIdentifier = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
        result.Add(nameIdentifier);

        var role = new Claim(ClaimTypes.Role, user.RoleId.ToString());
        result.Add(role);

        return result;
    }
}