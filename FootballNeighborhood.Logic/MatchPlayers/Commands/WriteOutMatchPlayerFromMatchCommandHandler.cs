using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.MatchPlayers.Commands;

public class WriteOutMatchPlayerFromMatchCommandHandler
    : ICommandHandler<WriteOutMatchPlayerFromMatchCommand, SuccessMessage>
{
    private readonly Context _context;

    public WriteOutMatchPlayerFromMatchCommandHandler(Context context)
    {
        _context = context;
    }


    public async Task<OperationResult<SuccessMessage>> Handle(WriteOutMatchPlayerFromMatchCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Validate(request);

        if (!result.Success) return result;

        var matchPlayer = await _context.MatchPlayers
            .SingleAsync(matchPlayer => matchPlayer.UserId == request.UserId
                                        && matchPlayer.MatchId == request.MatchId, cancellationToken);

        _context.MatchPlayers.Remove(matchPlayer);
        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }

    private async Task<OperationResult<SuccessMessage>> Validate(WriteOutMatchPlayerFromMatchCommand command)
    {
        var result = new OperationResult<SuccessMessage>();

        var match = await _context.Matches.FindAsync(command.MatchId);

        if (match is null) result.AddError(MatchesResources.MatchDoesNotExists_ErrorMessage);

        var userIsAssignedToMatch = await _context.MatchPlayers
            .AnyAsync(matchPlayer => matchPlayer.MatchId == command.MatchId
                                     && matchPlayer.UserId == command.UserId);

        if (!userIsAssignedToMatch) result.AddError(MatchPlayersResources.UserIsNotAssignedToMatch_ErrorMessage);

        return result;
    }
}