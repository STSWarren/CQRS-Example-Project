using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace DogSearch.Infrastructure.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly DatabaseConnectionOptions _options;
    private const string ShowTableName = "shows";
    private const string Id = "id";

    public ShowRepository(IOptions<DatabaseConnectionOptions> options)
    {
        _options = options.Value;
    }

    public async Task Create(Show show)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(ShowTableName).InsertAsync(new
        {
            id = show.Id.Value,
            name = show.Name,
            description = show.Description,
            date = show.Date,
            website = show.Website,
        });

        await connection.CloseAsync();
    }

    public async Task Delete(ShowId id)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(ShowTableName)
            .Where(Id, id.Value.ToString())
            .DeleteAsync());

        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Show>> GetAll()
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(ShowTableName)
            .GetAsync<Show>();

        await connection.CloseAsync();
        return result;
    }

    public async Task<Show> GetById(ShowId id)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(ShowTableName)
            .Where(Id, id.Value)
            .GetAsync<Show>())
            .First();

        await connection.CloseAsync();
        return result;
    }

    public async Task Update(ShowId id, Show show)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var showCurrentValue = (await db.Query(ShowTableName)
            .Where(Id, show.Id.Value)
            .GetAsync<Show>())
            .First();
        var updatedName = showCurrentValue.Name;
        if (show.Name != string.Empty)
        {
            updatedName = show.Name;
        }
        var updatedDesription = showCurrentValue.Description;
        if (show.Description != string.Empty)
        {
            updatedDesription = show.Description;
        }
        var updatedDate = showCurrentValue.Date;
        if (show.Date.Equals(new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero)))
        {
            updatedDate = show.Date;
        }
        var updatedWebsite = showCurrentValue.Website;
        if (show.Website != null)
        {
            updatedWebsite = show.Website;
        }
        await db.Query(ShowTableName).Where(Id, show.Id.Value).UpdateAsync(new
        {
            name = updatedName,
            description = updatedDesription,
            date = updatedDate,
            website = updatedWebsite,
        });
    }
}
