using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Dtos.Matches;
using FootballNeighborhood.Domain.Entities.Matches;
using FootballNeighborhood.Domain.Enums.Match;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class GetUserAssingedMatchesQueryHandler : IQueryHandler<GetUserAssingedMatchesQuery, GetUserAssingedMatchesQueryResult>
{
    private readonly Context _context;
    private readonly IUserContext _userContext;

    public GetUserAssingedMatchesQueryHandler(Context context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<GetUserAssingedMatchesQueryResult>> Handle(GetUserAssingedMatchesQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<GetUserAssingedMatchesQueryResult>();

        var userId = request.UserId ?? _userContext.CurrentUserId;

        var matches = await _context.Matches
            .Include(match => match.MatchPlayers)
            .ThenInclude(matchPLayer => matchPLayer.User)
            .Where(match => match!.MatchPlayers!
                .Any(matchPlayer => matchPlayer.UserId == userId))
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
                PlayerMatchStatus = GetPlayerStatus(match, userId),
                MatchPlayers = match.MatchPlayers.Any() ? match.MatchPlayers.Select(matchPlayer => new MatchPlayerDto()
                {
                    MatchPlayerId = matchPlayer.Id,
                    UserId = matchPlayer.UserId,
                    PlayerType = matchPlayer.PlayerType,
                    UserDisplayName = matchPlayer!.User!.Name + " " + matchPlayer!.User!.Surname
                }) : null
            })
            .ToListAsync(cancellationToken);

        result.Result = new GetUserAssingedMatchesQueryResult
        {
            Matches = matches
        };

        return result;
    }

    private static PlayerMatchStatusType? GetPlayerStatus(Match match, int currentUserId)
    {
        var currentTime = DateTimeOffset.UtcNow;
        var player = match.MatchPlayers
            .Single(matchPlayer => matchPlayer.UserId == currentUserId);

        if (match.IsFinished)
        {
            if (player.MatchReviewScore is not null) 
                return PlayerMatchStatusType.Reviewed;
            else return PlayerMatchStatusType.Completed;
        }
        else
        {
            if (currentTime < match.EndDateTime && currentTime < match.StartDateTime)
                return PlayerMatchStatusType.Upcoming;
            if (currentTime < match.EndDateTime && currentTime > match.StartDateTime)
                return PlayerMatchStatusType.Ongoing;
            if (currentTime > match.EndDateTime)
                return PlayerMatchStatusType.Expired;
        }

        return null;
    }
}