namespace FootballNeighborhood.Domain.Dtos.Authentications;

public record UserLoggedDto
{
    public UserLoggedDto(string token)
    {
        Token = token;
    }

    public string Token { get; }
}