using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Enums.Roles;
using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Authentications.Commands;

public record RegisterUserCommand : ICommand<SuccessMessageAndObjectId>
{
    public RegisterUserCommand(string email, string login, string password, Roles role)
    {
        Email = email;
        Login = login;
        Password = password;
        Role = role;
    }

    public string Email { get; }
    public string Login { get; }
    public string Password { get; }
    public Roles Role { get; }
}