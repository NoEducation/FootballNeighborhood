using FootballNeighborhood.Domain.Entities.Matches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNeighborhood.Domain.EntityTypeConfigurations.Matches;

public class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("Match", "dbo");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Matches)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.MatchPlayers)
            .WithOne(x => x.Match)
            .OnDelete(DeleteBehavior.Restrict);
    }
}