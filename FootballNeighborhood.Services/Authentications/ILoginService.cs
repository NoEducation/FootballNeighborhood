using FootballNeighborhood.Domain.Dtos.Authentications;
using FootballNeighborhood.Domain.Dtos.Common;

namespace FootballNeighborhood.Services.Authentications;

public interface ILoginService
{
    Task<OperationResult<UserLoggedDto>> Login(UserCredentialsDto credentials, CancellationToken cancellationToken);
}