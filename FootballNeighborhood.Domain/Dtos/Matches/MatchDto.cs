namespace FootballNeighborhood.Domain.Dtos.Matches;

public class MatchDto
{
    public int OwnerId { get; set; }

    public string OwnerDisplayName { get; set; } = default!;

    public string Name { get; set; } = default!;

    public bool IsFinished { get; set; }

    public DateTimeOffset StartDateTime { get; set; }

    public DateTimeOffset EndDateTime { get; set; }

    public string City { get; set; } = default!;

    public string AddressLine { get; set; } = default!;

    public short AllowedPlayers { get; set; }

    public short MinPlayers { get; set; }

    public bool ShowEmailAddress { get; set; }

    public bool ShowPhoneNumber { get; set; }

    public IEnumerable<MatchPlayerDto>? MatchPlayers { get; set; }
}