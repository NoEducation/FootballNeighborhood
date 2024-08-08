using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Logic.Users.Commands;
using FootballNeighborhood.Logic.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers;

public class UsersController : BaseController
{
    public UsersController(IDispatcher dispatcher) : base(dispatcher)
    {}

    [HttpGet("currentUser")]
    public async Task<OperationResult<GetCurrentUserQueryResult>> GetCurrentUser([FromQuery] int userId, CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetCurrentUserQuery(userId), cancellationToken);
    }

    [HttpPut("currentUser")]
    public async Task<OperationResult<SuccessMessage>> UpdateCurrentUser([FromBody] UpdateCurrentUserCommand command,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }
}

