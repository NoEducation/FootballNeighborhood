using FootballNeighborhood.DbMigrator;
using Microsoft.Extensions.Configuration;

string? connectionString;
IConfiguration? configuration = default!;
DbUpgrader upgrader = default!;

Initialization();
UpdateDatabase();
LogSuccessToConsole();

return 0;

void Initialization()
{
    connectionString = GetConnectionString();
    upgrader = new DbUpgrader();
}

void UpdateDatabase()
{
    var dropDatabase = configuration.GetValue<bool>("DropDatabase");

    try
    {
        Console.WriteLine("DB Update connectionString: " + connectionString);

        var result = upgrader.UpgradeDatabase(connectionString, dropDatabase);

        if (!result.Successful) FastHandleError(result.Error);

        foreach (var script in result.Scripts)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Executed script: {script.Name}");
        }
    }
    catch (Exception ex)
    {
        FastHandleError(ex);
    }
}

void FastHandleError(Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex);
    Console.ResetColor();

#if DEBUG
    Console.ReadLine();
#endif

    using var errorWriter = Console.Error;

    errorWriter.WriteLine(ex);
    errorWriter.Flush();

    Environment.Exit(-1);
}

void LogSuccessToConsole()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Db upgrade success!");
    Console.ResetColor();

#if DEBUG
    Console.ReadLine();
#endif
}

string? GetConnectionString()
{
    var connectionName = "DefaultConnection";

    configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", false)
        .AddJsonFile("appsettings.Development.json", true)
        .AddEnvironmentVariables()
        .Build();

    return configuration.GetConnectionString(connectionName);
}