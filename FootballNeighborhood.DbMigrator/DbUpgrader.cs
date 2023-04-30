using System.Reflection;
using DbUp;
using DbUp.Engine;

namespace FootballNeighborhood.DbMigrator;

public class DbUpgrader
{
    private readonly List<SqlScript> _allExecutedScripts = new();

    public DatabaseUpgradeResult UpgradeDatabase(string? connectionString, bool dropDatabase = false)
    {
        if (dropDatabase)
            DropDatabase.For.SqlDatabase(connectionString);

        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgradeResult = PerformUpgrade(connectionString);

        return new DatabaseUpgradeResult(_allExecutedScripts, upgradeResult.Successful, upgradeResult.Error,
            upgradeResult.ErrorScript);
    }

    private DatabaseUpgradeResult PerformUpgrade(string? connectionString)
    {
        //DeployChanges.To
        //    .SqlDatabase(connectionString)
        //    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        //    .LogToConsole()
        //    .Build();


        var builder = DeployChanges.To
            .SqlDatabase(connectionString)
            .LogToConsole()
            .WithTransaction()
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly());

        var result = builder.Build().PerformUpgrade();

        _allExecutedScripts.AddRange(result.Scripts);

        return result;
    }
}