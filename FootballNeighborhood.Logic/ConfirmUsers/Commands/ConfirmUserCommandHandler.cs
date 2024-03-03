using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Services.Contexts;
using Microsoft.AspNetCore.Components;

namespace FootballNeighborhood.Logic.ConfirmUsers.Commands;

public class ConfirmUserCommandHandler : ICommandHandler<ConfirmUserCommand, SuccessMessage>
{
    private readonly Context _context;

    public ConfirmUserCommandHandler(Context context)
    {
        _context = context;
    }

    public async Task<OperationResult<SuccessMessage>> Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    //private async Task<OperationResult> ValidateUserHasActiveConfirmation(ConfirmUserCommand request)
    //{
    //    var result = new OperationResult();

    //    var userHasActiveConfirmation = await _dispatcher.SendAsync(
    //        new CheckUserHasActiveConfirmationQuery()
    //        {
    //            UserId = request.UserId
    //        });

    //    if (!userHasActiveConfirmation.Success)
    //    {
    //        result.AddErrors(userHasActiveConfirmation.Errors);
    //        return result;
    //    }

    //    if (!userHasActiveConfirmation.Result.IsConfirmationActive)
    //    {
    //        result.AddError(_translateService.GetTranslatedMessage(nameof(ConfirmUserCommand), 1));
    //    }

    //    return result;
    //}
}
