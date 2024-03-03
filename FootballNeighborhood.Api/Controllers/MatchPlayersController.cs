using FootballNeighborhood.Domain.Consts.Permissions;
using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Infrastructure.Filters;
using FootballNeighborhood.Logic.MatchPlayers.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers
{
    public class MatchPlayersController : BaseController
    {
        public MatchPlayersController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.AssignToMatch })]
        [HttpPost("assingToMatch")]
        public async Task<OperationResult<SuccessMessage>> AssingToMatch([FromBody] AssignToMatchCommand command, CancellationToken cancellationToken)
        {
            return await DispatchAsync(command, cancellationToken);
        }

        [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.UnassignFromMatch })]
        [HttpPost("unassignFromMatch")]
        public async Task<OperationResult<SuccessMessage>> UnassignFromMatch(
            [FromBody] WriteOutMatchPlayerFromMatchCommand command,
            CancellationToken cancellationToken)
        {
            return await DispatchAsync(command, cancellationToken);
        }
    }

}
