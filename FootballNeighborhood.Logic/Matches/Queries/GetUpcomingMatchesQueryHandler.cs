using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Dtos.Matches;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class GetUpcomingMatchesQueryHandler : IQueryHandler<GetUpcomingMatchesQuery, GetUpcomingMatchesQueryResult>
{
    private readonly Context _context;
    private readonly IUserContext _userContext;

    public GetUpcomingMatchesQueryHandler(Context context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<GetUpcomingMatchesQueryResult>> Handle(GetUpcomingMatchesQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<GetUpcomingMatchesQueryResult>();

        var userId = request.UserId ?? _userContext.CurrentUserId;

        var matches = await _context.Matches
            .Include(match => match.MatchPlayers)
            .ThenInclude(matchPLayer => matchPLayer.User)
            .Select(match => new MatchDto
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
                MatchPlayers = match!.MatchPlayers!.Select(matchPlayer => new MatchPlayerDto
                {
                    UserId = matchPlayer.UserId,
                    MatchId = matchPlayer.MatchId,
                    PlayerType = matchPlayer.PlayerType,
                    UserDisplayName = matchPlayer!.User!.Name + " " + matchPlayer!.User!.Surname
                })
            })
            .Where(match => match!.MatchPlayers!
                .Any(matchPlayer => matchPlayer.UserId == userId))
            .ToListAsync(cancellationToken);

        result.Result = new GetUpcomingMatchesQueryResult
        {
            Matches = matches
        };

        return result;
    }
}