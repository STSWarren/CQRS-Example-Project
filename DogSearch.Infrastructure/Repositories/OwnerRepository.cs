using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace DogSearch.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly DatabaseConnectionOptions _options;
    private const string OwnerTableName = "owner";
    private const string Id = "id";

    public OwnerRepository(IOptions<DatabaseConnectionOptions> options)
    {
        _options = options.Value;
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
            .GetAsync<Owner>();

        await connection.CloseAsync();
        return result;
    }

    public async Task<Owner> GetById(OwnerId id)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var result = (await db.Query(OwnerTableName)
            .Where(Id, id.Value)
            .GetAsync<Owner>())
            .First();

        await connection.CloseAsync();
        return result;
    }

    public async Task Update(OwnerId id, Owner owner)
    {
        using var connection = new NpgsqlConnection(_options.DatabaseConnectionString);
        connection.Open();
        var compiler = new PostgresCompiler();
        var db = new QueryFactory(connection, compiler);

        var ownerCurrentValue = (await db.Query(OwnerTableName)
            .Where(Id, owner.Id.Value)
            .GetAsync<Owner>())
            .First();
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
