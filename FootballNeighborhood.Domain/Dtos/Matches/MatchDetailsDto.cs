namespace FootballNeighborhood.Domain.Dtos.Matches;

public class MatchDetailsDto : MatchDto
{
    public MatchOwnerInfoDto OwnerInfo { get; set; } = default!;
}

