using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Entities.Users;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Authentications;
using FootballNeighborhood.Services.Contexts;

namespace FootballNeighborhood.Logic.Authentications;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, SuccessMessageAndObjectId>
{
    private readonly Context _context;
    private readonly IPasswordHasherService _passwordHasherService;

    public RegisterUserCommandHandler(Context context,
        IPasswordHasherService passwordHasherService)
    {
        _context = context;
        _passwordHasherService = passwordHasherService;
    }

    public async Task<OperationResult<SuccessMessageAndObjectId>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<SuccessMessageAndObjectId>();

        //TODO.DA wysłanie wiadomości z potwierdzeniem

        var user = new User
        {
            RoleId = (int)request.Role,
            Login = request.Login,
            Password = _passwordHasherService.GenerateHash(request.Password),
            Email = request.Email,
            IsActive = false,
            IsConfirmed = false
        };

        _context.Users.Attach(user);

        await _context.SaveChangesAsync(cancellationToken);

        result.Result = new SuccessMessageAndObjectId
        {
            ObjectId = user.Id,
            Message = AuthenticationsRescource.UserRegistered_SuccessMessage
        };

        return result;
    }
}