using System.ComponentModel.DataAnnotations.Schema;
using FootballNeighborhood.Domain.Entities.Common;
using FootballNeighborhood.Domain.Entities.Users;
using FootballNeighborhood.Domain.Enums.Match;

namespace FootballNeighborhood.Domain.Entities.Matches;

[Table("MatchPlayer")]
public class MatchPlayer : EntityWithAdditionalUserInfo
{
    public int UserId { get; set; }
    public int MatchId { get; set; }
    public PlayerType PlayerType { get; set; }
    public virtual User? User { get; set; }
    public virtual Match? Match { get; set; }
}