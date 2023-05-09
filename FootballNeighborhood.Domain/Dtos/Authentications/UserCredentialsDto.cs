namespace FootballNeighborhood.Domain.Dtos.Authentications;

public record UserCredentialsDto
{
    public UserCredentialsDto(string password, string login)
    {
        Password = password;
        Login = login;
    }

    public string Password { get; }
    public string Login { get; }
}