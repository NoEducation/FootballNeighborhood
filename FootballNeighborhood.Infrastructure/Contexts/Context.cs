using FootballNeighborhood.Domain.Entities.Matches;
using FootballNeighborhood.Domain.Entities.Roles;
using FootballNeighborhood.Domain.Entities.Users;
using FootballNeighborhood.Domain.EntityTypeConfigurations.Roles;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Infrastructure.Contexts;

public class Context : DbContext
{
    private readonly string _connectionString;
    private readonly bool _inMemoryDatabase;

    public Context(string connectionString, bool inMemoryDatabase = false)
    {
        if (inMemoryDatabase == false && string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException(nameof(connectionString),
                "Error initializing context: connection string is empty !");

        _connectionString = connectionString;
        _inMemoryDatabase = inMemoryDatabase;
    }

    public DbSet<User> Users { get; protected set; }
    public DbSet<Role> Roles { get; protected set; }
    public DbSet<Role> Matches { get; protected set; }
    public DbSet<MatchPlayer> MatchPlayers { get; protected set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!_inMemoryDatabase)
            optionsBuilder.UseSqlServer(_connectionString);
        else
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleEntityTypeConfiguration).Assembly);
    }
}