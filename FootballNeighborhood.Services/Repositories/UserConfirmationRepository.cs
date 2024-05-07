
using FootballNeighborhood.Domain.Options;
using FootballNeighborhood.Services.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FootballNeighborhood.Services.Repositories;

public class UserConfirmationRepository : IUserConfirmationRepository
{
    private readonly Context _context;
    private readonly ConfirmationOptions _confirmationOptions;

    public UserConfirmationRepository(Context context,
        IOptions<ConfirmationOptions> confirmationOptions)
    {
        _context = context;
        _confirmationOptions = confirmationOptions.Value;
    }

    public async Task<bool> IsConfirmationActiveForUserId(int userId)
    {
        var validTill = DateTime.UtcNow;

        validTill = validTill.AddMinutes(-_confirmationOptions.ConfirmationValidTimeMinutes);

        var isConfirmationActive = await _context.UserConfirmations
            .AnyAsync(x => x.UserId == userId
                && x.IsUsed == false
                && validTill < x.CreatedDate);

        return isConfirmationActive;
    }
}

