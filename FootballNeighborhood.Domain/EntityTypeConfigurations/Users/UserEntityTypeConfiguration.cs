using FootballNeighborhood.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNeighborhood.Domain.EntityTypeConfigurations.Users;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", "dbo");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Gender)
               .HasColumnType("smallint");
    }
}