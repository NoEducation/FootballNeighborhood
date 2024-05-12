using FootballNeighborhood.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNeighborhood.Domain.EntityTypeConfigurations.Users;

public class UserConfirmationEntityTypeConfiguration : IEntityTypeConfiguration<UserConfirmation>
{
    public void Configure(EntityTypeBuilder<UserConfirmation> builder)
    {
        builder.ToTable("UserConfirmation", "dbo");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
            .WithMany(x => x.UserConfirmations)
            .OnDelete(DeleteBehavior.Restrict);

    }
}

