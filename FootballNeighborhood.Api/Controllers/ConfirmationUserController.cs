using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Logic.ConfirmUsers.Commands;
using FootballNeighborhood.Logic.ConfirmUsers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers;

[AllowAnonymous]
public class ConfirmationUserController : BaseController
{
    public ConfirmationUserController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpGet]
    public async Task<OperationResult<CheckUserHasActiveConfirmationQueryResult>> IsConfirmationActive([FromQuery] int userId)
    {
        var result = await DispatchAsync(
          new CheckUserHasActiveConfirmationQuery()
          {
              UserId = userId
          });

        return result;
    }

    [HttpPost("ConfirmUser")]
    public async Task<OperationResult> ConfirmUser([FromBody] ConfirmUserCommand confirmUserCommand)
    {
        var result = await DispatchAsync(confirmUserCommand);

        return result;
    }
}

