using DogSearch.Application;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app, app.Environment);
var config = app.Services.GetRequiredService<IConfiguration>();
var connection = config.GetValue<string>("ConnectionStrings:WebApiDatabase");
//var result = DbConfiguration.ConfigureDb(connection);
app.Run();