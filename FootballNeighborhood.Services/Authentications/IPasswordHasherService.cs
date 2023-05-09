namespace FootballNeighborhood.Services.Authentications;

public interface IPasswordHasherService
{
    public string GenerateHash(string password);
    public bool CompareHash(string target, string source);
}