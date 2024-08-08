using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.Users.Commands;

public class UpdateCurrentUserCommandHandler : ICommandHandler<UpdateCurrentUserCommand, SuccessMessage>
{
    private readonly Context _context;

    public UpdateCurrentUserCommandHandler(Context context)
    {
        _context = context;
    }

    public async Task<OperationResult<SuccessMessage>> Handle(UpdateCurrentUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);

        user!.Surname = request.Surname;
        user.Name = request.Name;
        user.Phone = request.Phone;
        user.BirthDate = request.BirthDate;
        user.Gender = request.Gender;
        user.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return new OperationResult<SuccessMessage>()
        {
            Result = new SuccessMessage()
            {
                Message = UsersResource.CurrentUserUpdated_SuccessMessage
            }
        };
    }
}
