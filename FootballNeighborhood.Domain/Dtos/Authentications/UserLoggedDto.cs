namespace FootballNeighborhood.Domain.Dtos.Authentications;

public record UserLoggedDto
{
    public UserLoggedDto(string token, int userId)
    {
        Token = token;
        UserId = userId;
    }

    public string Token { get; }
    public int UserId { get;  }
}