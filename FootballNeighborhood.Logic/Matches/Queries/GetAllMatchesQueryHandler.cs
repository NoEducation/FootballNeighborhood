using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Dtos.Matches;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Services.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class GetAllMatchesQueryHandler : IQueryHandler<GetAllMatchesQuery, GetAllMatchesQueryResult>
{
    private readonly Context _context;

    public GetAllMatchesQueryHandler(Context context)
    {
        _context = context;
    }

    public async Task<OperationResult<GetAllMatchesQueryResult>> Handle(GetAllMatchesQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<GetAllMatchesQueryResult>();

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
                ShowPhoneNumber = match.ShowPhoneNumber
            })
            .ToListAsync(cancellationToken);


        result.Result = new GetAllMatchesQueryResult
        {
            Matches = matches
        };

        return result;
    }
}