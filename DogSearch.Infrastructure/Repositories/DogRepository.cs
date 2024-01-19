using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace DogSearch.Infrastructure.Repositories;

public class DogRepository : IDogRepository
{
    private readonly DatabaseConnectionOptions _options;
    private const string DogTableName = "dogs";
    
    public DogRepository(IOptions<DatabaseConnectionOptions> options)
    {
        _options = options.Value;
    }

    public async Task Create(Dog dog)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(DogTableName).InsertAsync(new
        {
            id=dog.Id.Value,
            name = dog.Name,
            breed = dog.Breed,
            owner_id = dog.OwnerId,
            size= dog.Size,
        });

        await connection.CloseAsync();
    }

    public async Task Delete(DogId id)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(DogTableName)
            .Where("id", id.Value.ToString())
            .DeleteAsync());

        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Dog>> GetAll()
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(DogTableName)
            .GetAsync<Dog>();

        await connection.CloseAsync();
        return result;
    }

    public async Task<Dog> GetById(DogId id)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(DogTableName)
            .Where("id", id.Value)
            .GetAsync<Dog>())
            .First();

        await connection.CloseAsync();
        return result;
    }

    public Task Update(DogId id, Dog dog)
    {
        throw new NotImplementedException();
    }
}
