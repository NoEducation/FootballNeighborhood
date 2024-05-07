using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Options;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace FootballNeighborhood.Logic.ConfirmUsers.Commands;

public class ConfirmUserCommandHandler : ICommandHandler<ConfirmUserCommand, SuccessMessage>
{
    private readonly Context _context;
    private readonly IUserConfirmationRepository _userConfirmationRepository;
    private readonly ConfirmationOptions _confirmationOptions;

    public ConfirmUserCommandHandler(Context context,
        IUserConfirmationRepository userConfirmationRepository,
        IOptions<ConfirmationOptions> confirmationOptions)
    {
        _context = context;
        _userConfirmationRepository = userConfirmationRepository;
        _confirmationOptions = confirmationOptions.Value;
    }

    public async Task<OperationResult<SuccessMessage>> Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<SuccessMessage>();

        var isConfirmationActive = await _userConfirmationRepository
            .IsConfirmationActiveForUserId(request.UserId);

        if (!isConfirmationActive)
        {
            result.AddError(UserConfirmationsResource.NoActiveConfirmation_ErrorMessage);
            return result;
        }

        var validTill = DateTime.UtcNow;

        validTill = validTill.AddMinutes(-_confirmationOptions.ConfirmationValidTimeMinutes);

        var currentConfirmation = await _context
            .UserConfirmations.SingleAsync(x => x.UserId == request.UserId
                && x.IsUsed == false
                && validTill < x.CreatedDate);

        currentConfirmation.Code = WebUtility.UrlDecode(currentConfirmation.Code);

        if (currentConfirmation.Code != request.Code)
        {
            result.AddError(UserConfirmationsResource.InvalidCode_ErrorMessage);
            return result;
        }

        var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == request.UserId);

        if (user!.IsConfirmed)
        {
            result.AddError(UserConfirmationsResource.UserAlreadyConfirmed_ErrorMessage);
            return result;
        }

        user.IsConfirmed = true;

        currentConfirmation.IsUsed = true;
        currentConfirmation.UsedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        result.Result = new SuccessMessage()
        {
            Message = UserConfirmationsResource.EmailAddressSuccessfullyConfirmed
        };

        return result;
    }
}
