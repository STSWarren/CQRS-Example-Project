using AutoMapper;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Infrastructure.Dtos.Dogs;
using DogSearch.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace DogSearch.Infrastructure.Repositories;

public class DogRepository : IDogRepository
{
    private readonly DatabaseConnectionOptions _options;
    private readonly IMapper _mapper;
    private const string DogTableName = "dogs";
    private const string Id = "id";
    
    public DogRepository(IOptions<DatabaseConnectionOptions> options, IMapper mapper)
    {
        _options = options.Value;
        _mapper = mapper;
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
            owner_id = dog.OwnerId.Value,
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
            .Where(Id, id.Value.ToString())
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
            .GetAsync<DogDto>();

        await connection.CloseAsync();
        return _mapper.Map<IEnumerable<Dog>>(result);
    }

    public async Task<Dog> GetById(DogId id)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(DogTableName)
            .Where(Id, id.Value)
            .GetAsync<DogDto>())
            .First();

        await connection.CloseAsync();
        return _mapper.Map<Dog>(result);
    }

    public async Task<IEnumerable<Dog>> ListByIds(IEnumerable<DogId> ids)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(DogTableName)
            .Where(Id, ids.Select(x => x.Value))
            .GetAsync<DogDto>();

        await connection.CloseAsync();
        return _mapper.Map<IEnumerable<Dog>>(result);
    }

    public async Task Update(DogId id, Dog dog)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var dogCurrentValueDto = (await db.Query(DogTableName)
            .Where(Id, dog.Id.Value)
            .GetAsync<DogDto>())
            .First();
        var dogCurrentValue = _mapper.Map<Dog>(dogCurrentValueDto);
        var updatedName = dogCurrentValue.Name;
        if (dog.Name != string.Empty)
        {
            updatedName = dog.Name;
        }
        var updatedBreed = dogCurrentValue.Breed;
        if (dog.Breed != string.Empty)
        {
            updatedBreed = dog.Breed;
        }
        var updatedOwnerId = dogCurrentValue.OwnerId;
        if (dog.OwnerId.Value != Guid.Empty)
        {
            updatedOwnerId = dog.OwnerId;
        }
        var updatedSize = dogCurrentValue.Size;
        if (dog.Size != null)
        {
            updatedSize = dog.Size;
        }
        await db.Query(DogTableName).Where(Id, dog.Id.Value).UpdateAsync(new
        {
            name = updatedName,
            breed = updatedBreed,
            owner_id = updatedOwnerId,
            size = updatedSize,
        });
    }
}