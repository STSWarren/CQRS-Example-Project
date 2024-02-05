using AutoMapper;
using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Infrastructure.Dtos.Owners;
using DogSearch.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace DogSearch.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly DatabaseConnectionOptions _options;
    private readonly IMapper _mapper;
    private const string OwnerTableName = "owners";
    private const string Id = "id";

    public OwnerRepository(IOptions<DatabaseConnectionOptions> options, IMapper mapper)
    {
        _options = options.Value;
        _mapper = mapper;
    }

    public async Task Create(Owner owner)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(OwnerTableName).InsertAsync(new
        {
            id = owner.Id.Value,
            first_name = owner.FirstName,
            last_name = owner.LastName,
            address = owner.Address,
            email = owner.Email,
            phone = owner.Phone,
        });

        await connection.CloseAsync();
    }

    public async Task Delete(OwnerId id)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(OwnerTableName)
            .Where(Id, id.Value.ToString())
            .DeleteAsync());

        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Owner>> GetAll()
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = await db.Query(OwnerTableName)
            .GetAsync<OwnerDto>();

        await connection.CloseAsync();
        return _mapper.Map<IEnumerable<Owner>>(result);
    }

    public async Task<Owner> GetById(OwnerId id)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(OwnerTableName)
            .Where(Id, id.Value)
            .GetAsync<OwnerDto>())
            .First();

        await connection.CloseAsync();
        return _mapper.Map<Owner>(result);
    }

    public async Task Update(OwnerId id, Owner owner)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var ownerCurrentValueDto = (await db.Query(OwnerTableName)
            .Where(Id, owner.Id.Value)
            .GetAsync<OwnerDto>())
            .First();
        var ownerCurrentValue = _mapper.Map<Owner>(ownerCurrentValueDto);
        var updatedFirstName = ownerCurrentValue.FirstName;
        if (owner.FirstName != string.Empty)
        {
            updatedFirstName = owner.FirstName;
        }
        var updatedLastName = ownerCurrentValue.LastName;
        if (owner.LastName != string.Empty)
        {
            updatedLastName = owner.LastName;
        }
        var updatedAddress = ownerCurrentValue.Address;
        if (owner.Address != string.Empty)
        {
            updatedAddress = owner.Address;
        }
        var updatedEmail = ownerCurrentValue.Email;
        if (owner.Email != null)
        {
            updatedEmail = owner.Email;
        }
        var updatedPhone = ownerCurrentValue.Phone;
        if (owner.Phone != null)
        {
            updatedPhone = owner.Phone;
        }
        await db.Query(OwnerTableName).Where(Id, owner.Id.Value).UpdateAsync(new
        {
            first_name = updatedFirstName,
            last_name = updatedLastName,
            address = updatedAddress,
            email = updatedEmail,
            phone = updatedPhone,
        });
    }
}
