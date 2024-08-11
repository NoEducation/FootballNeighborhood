using FootballNeighborhood.Domain.Entities.Common;

namespace FootballNeighborhood.Domain.Entities.Matches;

public class MatchPlayerReview : EntityWithAdditionalUserInfo
{
    public int ReviewedPlayerId { get; set; }
    public short Score { get; set; }
    public string? Description { get; set; }
    public int MatchPlayerId { get; set; }
    public virtual MatchPlayer? MatchPlayer { get; set; }
    public virtual MatchPlayer? ReviewedPlayer { get; set; }
}

