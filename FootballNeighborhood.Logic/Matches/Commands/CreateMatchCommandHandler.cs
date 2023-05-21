using FootballNeighborhood.Domain.Consts.Permissions;
using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Entities.Matches;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;

namespace FootballNeighborhood.Logic.Matches.Commands;

public class CreateMatchCommandHandler : ICommandHandler<CreateMatchCommand, SuccessMessageAndObjectId>
{
    private readonly Context _context;
    private readonly IUserContext _userContext;

    public CreateMatchCommandHandler(Context context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<SuccessMessageAndObjectId>> Handle(CreateMatchCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<SuccessMessageAndObjectId>();

        if (!await _userContext.UserHasPermission(Permissions.SaveMatch))
        {
            result.AddError(MatchesResources.PlayerCannotCreateMaches_ErrorMessage);
            return result;
        }

        var match = new Match
        {
            OwnerId = _userContext.CurrentUserId,
            Name = request.Name,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            City = request.City,
            AddressLine = request.AddressLine,
            AllowedPlayers = request.AllowedPlayers,
            MinPlayers = request.MinPlayers,
            ShowEmailAddress = request.ShowEmailAddress,
            ShowPhoneNumber = request.ShowPhoneNumber
        };

        _context.Matches.Attach(match);
        await _context.SaveChangesAsync(cancellationToken);

        result.Result = new SuccessMessageAndObjectId
        {
            ObjectId = match.Id,
            Message = MatchesResources.MatchHasBeenCreated
        };

        return result;
    }
}