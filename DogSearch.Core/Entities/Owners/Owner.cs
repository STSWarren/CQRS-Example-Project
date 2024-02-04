namespace DogSearch.Core.Entities.Owners;

public class Owner
{
    public Owner(Guid id, string firstName, string lastName, string address, string email, string phone)
    {
        Id = new OwnerId(id);
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Email = email;
        Phone = phone;
    }

    public OwnerId Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Address { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
}