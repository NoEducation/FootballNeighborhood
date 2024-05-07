namespace FootballNeighborhood.Domain.Options;

public class TokenOptions
{
    public static string Key => "TokenOptions";

    public string Secrete { get; set; } = default!;
    public string Salt { get; set; } = default!;
    public short AccessTokenTimeValid { get; set; }
    public short RefreshTokenTimeValid { get; set; }
}