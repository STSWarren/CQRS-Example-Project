namespace DogSearch.Infrastructure.Options;

public class DatabaseConnectionOptions
{

    public const string Section = "ConnectionStrings";
    public string DatabaseConnectionString { get; set; } = String.Empty;
}
