using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Logic.ConfirmUsers.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers;

public class ConfirmationUserController : BaseController
{
    public ConfirmationUserController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [AllowAnonymous]
    [HttpPost("ConfirmUser")]
    public async Task<OperationResult> ConfirmUser([FromBody] ConfirmUserCommand confirmUserCommand)
    {
        var result = await DispatchAsync(confirmUserCommand);

        return result;
    }
}

