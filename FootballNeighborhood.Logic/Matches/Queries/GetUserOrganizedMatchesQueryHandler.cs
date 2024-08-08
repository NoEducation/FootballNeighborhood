using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Dtos.Matches;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class GetUserOrganizedMatchesQueryHandler : IQueryHandler<GetUserOrganizedMatchesQuery, GetUserOrganizedMatchesQueryResult>
{
    private readonly Context _context;
    private readonly IUserContext _userContext;

    public GetUserOrganizedMatchesQueryHandler(Context context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<GetUserOrganizedMatchesQueryResult>> Handle(GetUserOrganizedMatchesQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<GetUserOrganizedMatchesQueryResult>();

        var userId = request.UserId ?? _userContext.CurrentUserId;

        var matches = await _context.Matches
            .Include(match => match.MatchPlayers)
            .ThenInclude(matchPLayer => matchPLayer.User)
            .Where(match => match.OwnerId == request.UserId)
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
                MatchPlayers = match.MatchPlayers.Any() ? match.MatchPlayers.Select(matchPlayer => new MatchPlayerDto()
                {
                    MatchPlayerId = matchPlayer.Id,
                    UserId = matchPlayer.UserId,
                    PlayerType = matchPlayer.PlayerType,
                    UserDisplayName = matchPlayer!.User!.Name + " " + matchPlayer!.User!.Surname
                }) : null
            })
            .ToListAsync(cancellationToken);

        result.Result = new GetUserOrganizedMatchesQueryResult
        {
            Matches = matches
        };

        return result;
    }
}

