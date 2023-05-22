using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Entities.Matches;
using FootballNeighborhood.Domain.Enums.Match;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.MatchPlayers.Commands;

public class AssignToMatchCommandHandler : ICommandHandler<AssignToMatchCommand, SuccessMessage>
{
    private readonly Context _context;
    private readonly IUserContext _userContext;

    public AssignToMatchCommandHandler(Context context,
        IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<SuccessMessage>> Handle(AssignToMatchCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Validate(request);

        if (!result.Success) return result;

        var match = await _context.Matches.FindAsync(request.MatchId);

        var currentPlayerNumber = await _context.MatchPlayers
            .CountAsync(matchPlayer => matchPlayer.MatchId == request.MatchId
                                       && matchPlayer.PlayerType == PlayerType.Playing, cancellationToken);

        var matchPlayer = new MatchPlayer
        {
            MatchId = request.MatchId,
            PlayerType = currentPlayerNumber > match!.AllowedPlayers ? PlayerType.Reserve : PlayerType.Playing,
            UserId = _userContext.CurrentUserId
        };

        _context.MatchPlayers.Attach(matchPlayer);
        await _context.SaveChangesAsync(cancellationToken);

        result.Result = new SuccessMessage
        {
            Message = MatchPlayersResources.UserHasBeenAssignedToMatch
        };

        return result;
    }


    private async Task<OperationResult<SuccessMessage>> Validate(AssignToMatchCommand command)
    {
        var result = new OperationResult<SuccessMessage>();

        var match = await _context.Matches.FindAsync(command.MatchId);

        if (match is null) result.AddError(MatchesResources.MatchDoesNotExists_ErrorMessage);

        var userIsAssignedToMatch = await _context.MatchPlayers
            .AnyAsync(matchPlayer => matchPlayer.MatchId == command.MatchId
                                     && matchPlayer.UserId == _userContext.CurrentUserId);

        if (userIsAssignedToMatch) result.AddError(MatchPlayersResources.UserAlreadyAssignedToMatch_ErrorMessage);


        return result;
    }
}