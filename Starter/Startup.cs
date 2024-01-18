using DogSearch.Core;
using DogSearch.Infrastructure;

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
        services.AddInfrastructureServices();
        services.AddCoreServices();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
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
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}