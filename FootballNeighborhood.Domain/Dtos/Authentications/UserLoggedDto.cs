using FootballNeighborhood.Domain.Enums.Roles;

namespace FootballNeighborhood.Domain.Dtos.Authentications;

public record UserLoggedDto
{
    public UserLoggedDto(string token, int userId, RolesEnum role)
    {
        Token = token;
        UserId = userId;
        Role = role;
    }

    public string Token { get; }
    public int UserId { get;  }
    public RolesEnum Role { get; }
}