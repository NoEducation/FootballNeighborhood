using FootballNeighborhood.Domain.Entities.Matches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNeighborhood.Domain.EntityTypeConfigurations.Matches;

public class MatchPlayerEntityTypeConfiguration : IEntityTypeConfiguration<MatchPlayer>
{
    public void Configure(EntityTypeBuilder<MatchPlayer> builder)
    {
        builder.HasKey(x => x.Id);
    }
}