using FootballNeighborhood.Domain.Entities.Matches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNeighborhood.Domain.EntityTypeConfigurations.Matches;

public class MatchPlayerReviewEntityTypeConfiguration : IEntityTypeConfiguration<MatchPlayerReview>
{
    public void Configure(EntityTypeBuilder<MatchPlayerReview> builder)
    {
        builder.ToTable("MatchPlayerReview", "dbo");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Score)
            .HasColumnType("smallint");
    }
}

