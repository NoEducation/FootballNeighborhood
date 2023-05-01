using FootballNeighborhood.Domain.Entities.Permissions;
using FootballNeighborhood.Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNeighborhood.Domain.EntityTypeConfigurations.Roles;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasMany(x => x.Permissions)
            .WithMany(x => x.Roles)
            .UsingEntity(nameof(RolePermission),
                left => left.HasOne(typeof(Permission)).WithMany()
                    .HasForeignKey("PermissionId").HasPrincipalKey(nameof(Permission.Id)),
                right => right.HasOne(typeof(Role)).WithMany()
                    .HasForeignKey("RoleId").HasPrincipalKey(nameof(Role.Id)),
                join => join.HasKey(nameof(RolePermission.RoleId), nameof(RolePermission.PermissionId)))
            ;
    }
}