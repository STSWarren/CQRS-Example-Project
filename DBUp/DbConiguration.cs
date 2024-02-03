using DbUp;
using System.Reflection;

public static class DbConfiguration
{
    public static object ConfigureDb(string connectionstring)
    {
        var upgrader =
            DeployChanges.To
                         .PostgresqlDatabase(connectionstring)
                         .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                         .LogToConsole()
                         .Build();
        return upgrader.PerformUpgrade();
    }
}