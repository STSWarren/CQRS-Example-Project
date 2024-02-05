using AutoMapper;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Infrastructure.Dtos.Placements;
using DogSearch.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace DogSearch.Infrastructure.Repositories;

public class PlacementRepository : IPlacementRepository
{
    private readonly DatabaseConnectionOptions _options;
    private readonly IMapper _mapper;
    private const string PlacementTableName = "placements";
    private const string ShowId = "show_id";
    private const string DogId = "dog_id";
    private const string Category = "category";

    public PlacementRepository(IOptions<DatabaseConnectionOptions> options, IMapper mapper)
    {
        _options = options.Value;
        _mapper = mapper;
    }

    public async Task Create(Placement placement)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(PlacementTableName).InsertAsync(new
        {
            id = placement.Id,
            show_id = placement.ShowId.Value,
            dog_id = placement.DogId.Value,
            category = placement.Category,
            place = placement.Place,
        });

        await connection.CloseAsync();
    }

    public async Task Delete(DogId dogId, ShowId showId, string category)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(PlacementTableName)
            .Where(ShowId, showId.Value.ToString())
            .Where(DogId, dogId.Value.ToString())
            .Where(Category, category)
            .DeleteAsync());

        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Placement>> GetAll()
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(PlacementTableName)
            .GetAsync<PlacementDto>();

        await connection.CloseAsync();
        return _mapper.Map<IEnumerable<Placement>>(result);
    }

    public async Task<IEnumerable<Placement>> GetAllByDogId(DogId dogId)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(PlacementTableName)
            .Where(DogId, dogId.Value.ToString())
            .GetAsync<PlacementDto>();

        await connection.CloseAsync();
        return _mapper.Map<IEnumerable<Placement>>(result);
    }

    public async Task<IEnumerable<Placement>> GetAllByShowId(ShowId showId)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(PlacementTableName)
            .Where(ShowId, showId.Value.ToString())
            .GetAsync<PlacementDto>();

        await connection.CloseAsync();
        return _mapper.Map<IEnumerable<Placement>>(result);
    }

    public async Task<IEnumerable<Placement>> GetAllByShowIdAndDogId(ShowId showId, DogId dogId)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(PlacementTableName)
            .Where(ShowId, showId.Value.ToString())
            .Where(DogId, dogId.Value.ToString())
            .GetAsync<PlacementDto>();

        await connection.CloseAsync();
        return _mapper.Map<IEnumerable<Placement>>(result);
    }

    public async Task<IEnumerable<Placement>> GetAllByShowIdAndCategory(ShowId showId, string category)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(PlacementTableName)
            .Where(ShowId, showId.Value.ToString())
            .Where(Category, category)
            .GetAsync<PlacementDto>();

        await connection.CloseAsync();
        return _mapper.Map<IEnumerable<Placement>>(result);
    }

    public async Task Update(DogId dogId, ShowId showId, string category, int place)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);
        await db.Query(PlacementTableName)
            .Where(ShowId, showId.Value.ToString())
            .Where(DogId, dogId.Value.ToString())
            .Where(Category, category)
            .UpdateAsync(new
        {
            place = place,
        });
    }

    public async Task<Placement> Get(DogId dogId, ShowId showId, string category)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(PlacementTableName)
            .Where(ShowId, showId.Value.ToString())
            .Where(DogId, dogId.Value.ToString())
            .Where(Category, category)
            .GetAsync<PlacementDto>())
            .First();

        await connection.CloseAsync();
        return _mapper.Map<Placement>(result);
    }
}
