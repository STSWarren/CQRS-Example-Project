using AutoMapper;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Infrastructure.Dtos.Shows;
using DogSearch.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace DogSearch.Infrastructure.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly DatabaseConnectionOptions _options;
    private readonly IMapper _mapper;
    private const string ShowTableName = "shows";
    private const string Id = "id";

    public ShowRepository(IOptions<DatabaseConnectionOptions> options, IMapper mapper)
    {
        _options = options.Value;
        _mapper = mapper;
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
            date = show.Date.ToUniversalTime(),
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
            .GetAsync<ShowDto>();

        await connection.CloseAsync();
        return _mapper.Map<IEnumerable<Show>>(result);
    }

    public async Task<Show> GetById(ShowId id)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(ShowTableName)
            .Where(Id, id.Value)
            .GetAsync<ShowDto>())
            .First();

        await connection.CloseAsync();
        return _mapper.Map<Show>(result);
    }

    public async Task Update(ShowId id, Show show)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var showCurrentValueDto = (await db.Query(ShowTableName)
            .Where(Id, show.Id.Value)
            .GetAsync<ShowDto>())
            .First();
        var showCurrentValue = _mapper.Map<Show>(showCurrentValueDto);
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
        if (show.Website != string.Empty)
        {
            updatedWebsite = show.Website;
        }
        await db.Query(ShowTableName).Where(Id, show.Id.Value).UpdateAsync(new
        {
            name = updatedName,
            description = updatedDesription,
            date = updatedDate.ToUniversalTime(),
            website = updatedWebsite,
        });
    }

    public async Task<IEnumerable<Show>> ListByIds(IEnumerable<ShowId> ids)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(ShowTableName)
            .Where(Id, ids.Select(x => x.Value))
            .GetAsync<ShowDto>();

        await connection.CloseAsync();
        return _mapper.Map<IEnumerable<Show>>(result);
    }
}
