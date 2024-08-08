using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Enums.Roles;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Services.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.Users.Queries;

public class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, GetCurrentUserQueryResult>
{
    private readonly Context _context;

    public GetCurrentUserQueryHandler(Context context)
    {
        _context = context;
    }

    public async Task<OperationResult<GetCurrentUserQueryResult>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(user => user.Id == request.UserId);

        //TODO.DA get scores by other players
        //var matches = await _context.Matches
        //    .Include(match => match.MatchPlayers)
        //    .ThenInclude(matchPlayer => matchPlayer.)
        //    .AsNoTracking()
        //    .Where(match
        //    => match.MatchPlayers.Any(matchPlayer => matchPlayer.Id == user.Id))
        //    .Select();

        var result = new OperationResult<GetCurrentUserQueryResult>()
        {
            Result = new GetCurrentUserQueryResult()
            {
                UserId = request.UserId,
                Surname = user!.Surname!,
                Email = user.Email,
                Name = user.Name!,
                Role = user.RoleId switch
                {
                    (int)RolesEnum.Player => RolesEnum.Player,
                    (int)RolesEnum.MatchOrganizer => RolesEnum.MatchOrganizer,
                    (int)RolesEnum.Admin => RolesEnum.Admin,
                    _ => throw new ArgumentException($"Please add mapping for role: {user.RoleId}")
                },
                Phone = user.Phone!,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Description = user.Description!
                
            }
        };

        return result;
    }
}

