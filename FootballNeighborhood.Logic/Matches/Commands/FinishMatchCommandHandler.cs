using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;

namespace FootballNeighborhood.Logic.Matches.Commands;

public class FinishMatchCommandHandler : ICommandHandler<FinishMatchCommand, SuccessMessage>
{
    private readonly Context _context;
    private readonly UserContext _userContext;

    public FinishMatchCommandHandler(Context context,
        UserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<SuccessMessage>> Handle(FinishMatchCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<SuccessMessage>();

        var match = await _context.Matches.FindAsync(request.MatchId, cancellationToken);

        if (match!.IsFinished)
        {
            result.AddError(MatchesResources.MatchAlreadyFinished_ErrorMessage);
            return result;
        }

        match!.IsFinished = true;
        match.SetModificationInfo(_userContext.CurrentUserId, DateTimeOffset.UtcNow);

        await _context.SaveChangesAsync(cancellationToken);

        result.Result = new SuccessMessage()
        {
            Message = MatchesResources.MatchFinished_SuccessMessage
        };

        return result;
    }
}

