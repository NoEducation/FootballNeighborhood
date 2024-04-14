using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Dtos.Matches;
using FootballNeighborhood.Domain.Enums.Match;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class GetMatchByIdQueryHandler : IQueryHandler<GetMatchByIdQuery, GetMatchByIdQueryResult>
{
    private readonly Context _context;

    public GetMatchByIdQueryHandler(Context context)
    {
        _context = context;
    }

    public async Task<OperationResult<GetMatchByIdQueryResult>> Handle(GetMatchByIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<GetMatchByIdQueryResult>();

        var match = await _context.Matches
            .Include(match => match.MatchPlayers)
                .ThenInclude(matchPLayer => matchPLayer.User)
            .Include(match => match.Owner)
            .SingleOrDefaultAsync(match => match.Id == request.MatchId, cancellationToken);

        if (match is null)
        {
            result.AddError(MatchesResources.MatchDoesNotExists_ErrorMessage);
            return result;
        }

        result.Result = new GetMatchByIdQueryResult
        {
            Match = new MatchDto
            {
                MatchId = match.Id,
                OwnerId = match.OwnerId,
                OwnerDisplayName = match!.Owner!.Name + " " + match!.Owner!.Surname,
                Name = match.Name,
                IsFinished = match.IsFinished,
                StartDateTime = match.StartDateTime,
                EndDateTime = match.EndDateTime,
                City = match.City,
                AddressLine = match.AddressLine,
                AllowedPlayers = match.AllowedPlayers,
                ShowEmailAddress = match.ShowEmailAddress,
                ShowPhoneNumber = match.ShowPhoneNumber,
                MatchPlayers = match!.MatchPlayers?.Any() == true ? match!.MatchPlayers
                    .OrderBy(player => player.PlayerType == PlayerType.Playing)
                        .ThenBy(player => player.AddedDate)
                    .Select(matchPlayer => new MatchPlayerDto
                {
                    MatchPlayerId = matchPlayer.Id,
                    UserId = matchPlayer.UserId,
                    MatchId = matchPlayer.MatchId,
                    PlayerType = matchPlayer.PlayerType,
                    UserDisplayName = matchPlayer!.User!.Name + " " + matchPlayer!.User!.Surname
                }) : new List<MatchPlayerDto>()
            }
        };

        return result;
    }
}