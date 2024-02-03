using DogSearch.Core;
using DogSearch.Infrastructure;
using DogSearch.Infrastructure.Options;

namespace DogSearch.Application;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddOptions<DatabaseConnectionOptions>()
            .Bind(Configuration.GetSection(DatabaseConnectionOptions.Section));

        services.AddInfrastructureServices();
        services.AddCoreServices();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options => {
            options.AddPolicy("CorsPolicy", policy => {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseStatusCodePagesWithReExecute("/error/{0}");
        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseSwagger(); 
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = "";
        });
        var result = DbConfiguration.ConfigureDb(
            Configuration.GetValue<string>("ConnectionStrings:DatabaseConnectionString"));
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}