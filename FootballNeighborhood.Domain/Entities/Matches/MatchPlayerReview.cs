using FootballNeighborhood.Domain.Entities.Common;
using FootballNeighborhood.Domain.Entities.Users;

namespace FootballNeighborhood.Domain.Entities.Matches;

public class MatchPlayerReview : EntityWithAdditionalUserInfo
{
    public int ReviewedUserId { get; set; }
    public int ReviewedByUserId { get; set; }
    public int MatchId { get; set; }
    public short Score { get; set; }
    public string? Description { get; set; }
    public virtual Match? Match { get; set; }
    public virtual User? ReviewedUser { get; set; }
    public virtual User? ReviewedByUser { get; set; }
}

