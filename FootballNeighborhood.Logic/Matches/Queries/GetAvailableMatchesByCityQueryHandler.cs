using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Dtos.Matches;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class
    GetAvailableMatchesByCityQueryHandler : IQueryHandler<GetAvailableMatchesByCityQuery,
        GetAvailableMatchesByCityQueryResult>
{
    private readonly Context _context;
    private readonly IUserContext _userContext;

    public GetAvailableMatchesByCityQueryHandler(Context context, 
        IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<GetAvailableMatchesByCityQueryResult>> Handle(
        GetAvailableMatchesByCityQuery request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<GetAvailableMatchesByCityQueryResult>();

        var matches = await _context.Matches
            .Include(match => match.Owner)
            .Include(match => match.MatchPlayers)
            .ThenInclude(matchPLayer => matchPLayer.User)
            .Where(match => match.IsFinished == false
                            //&& match.MatchPlayers.All(matchPlayer => matchPlayer.UserId != _userContext.CurrentUserId)
                            && match.EndDateTime > DateTimeOffset.Now
                            && match.City.ToLower().Contains(request.City.ToLower()))
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
            .ToListAsync(cancellationToken);


        result.Result = new GetAvailableMatchesByCityQueryResult
        {
            Matches = matches
        };

        return result;
    }
}