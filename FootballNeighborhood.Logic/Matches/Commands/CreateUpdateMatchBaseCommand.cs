namespace FootballNeighborhood.Logic.Matches.Commands;

public record CreateUpdateMatchBaseCommand
{
    public string Name { get; protected set; } = string.Empty;
    public DateTimeOffset StartDateTime { get; protected set; }
    public DateTimeOffset EndDateTime { get; protected set; }

    public string City { get; protected set; } = string.Empty;
    public string AddressLine { get; protected set; } = string.Empty;

    public short AllowedPlayers { get; protected set; }
    public short MinPlayers { get; protected set; }

    public bool ShowEmailAddress { get; protected set; }
    public bool ShowPhoneNumber { get; protected set; }
}