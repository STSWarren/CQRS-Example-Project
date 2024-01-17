using DbUp;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();
var config = app.Services.GetRequiredService<IConfiguration>();
var connection = config.GetValue<string>("ConnectionStrings:WebApiDatabase"); 
var upgrader =
    DeployChanges.To
                 .PostgresqlDatabase(connection)
                 .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                 .LogToConsole()
                 .Build();
var result = upgrader.PerformUpgrade();

app.Run();
