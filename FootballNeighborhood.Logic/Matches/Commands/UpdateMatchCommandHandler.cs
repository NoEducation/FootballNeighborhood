using FootballNeighborhood.Domain.Consts.Permissions;
using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Entities.Matches;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;

namespace FootballNeighborhood.Logic.Matches.Commands;

public class UpdateMatchCommandHandler : ICommandHandler<UpdateMatchCommand, SuccessMessage>
{
    private readonly Context _context;

    private readonly IUserContext _userContext;

    public UpdateMatchCommandHandler(Context context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<SuccessMessage>> Handle(UpdateMatchCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Validate(request);

        if (!result.Success) return result;

        var match = await _context.Matches.FindAsync(request.MatchId);

        SetProperties(request, match!);

        await _context.SaveChangesAsync(cancellationToken);

        result.Result = new SuccessMessage
        {
            Message = MatchesResources.MatchHasBeenUpdated
        };

        return result;
    }

    private void SetProperties(UpdateMatchCommand command, Match match)
    {
        match.Name = command.Name;
        match.StartDateTime = command.StartDateTime;
        match.EndDateTime = command.EndDateTime;
        match.City = command.City;
        match.AddressLine = command.AddressLine;
        match.AllowedPlayers = command.AllowedPlayers;
        match.MinPlayers = command.MinPlayers;
        match.ShowEmailAddress = command.ShowEmailAddress;
        match.ShowPhoneNumber = command.ShowPhoneNumber;
    }

    private async Task<OperationResult<SuccessMessage>> Validate(UpdateMatchCommand command)
    {
        var result = new OperationResult<SuccessMessage>();

        var match = await _context.Matches.FindAsync(command.MatchId);

        if (match is null)
        {
            result.AddError(MatchesResources.MatchDoesNotExists_ErrorMessage);
            return result;
        }

        if (!await _userContext.UserHasPermission(Permissions.EditAnotherUserMatch)
            || _userContext.CurrentUserId != match.OwnerId)
            result.AddError(MatchesResources.InsufficientPrivileges_ErrorMessage);

        return result;
    }
}