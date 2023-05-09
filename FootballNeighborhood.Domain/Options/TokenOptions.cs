namespace FootballNeighborhood.Domain.Options;

public class TokenOptions
{
    public static string Key => "TokenOptions";

    public string Secrete { get; set; }
    public string Salt { get; set; }
    public short AccessTokenTimeValid { get; set; }
    public short RefreshTokenTimeValid { get; set; }
}