using System.ComponentModel.DataAnnotations.Schema;
using FootballNeighborhood.Domain.Entities.Common;
using FootballNeighborhood.Domain.Entities.Users;

namespace FootballNeighborhood.Domain.Entities.Matches;

[Table("Match")]
public class Match : EntityWithAdditionalUserInfo
{
    public int OwnerId { get; set; }

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

    public virtual User? Owner { get; set; }

    public virtual ICollection<MatchPlayer>? MatchPlayers { get; set; }
}