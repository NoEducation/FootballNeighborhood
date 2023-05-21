using FootballNeighborhood.Domain.Enums.Roles;

namespace FootballNeighborhood.Services.UserContext;

public interface IUserContext
{
    public int CurrentUserId { get; }
    public Roles Role { get; }
    Task<bool> UserHasPermission(string target);
}