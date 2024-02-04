using DogSearch.Core.Entities.Owners;
using MediatR;

namespace DogSearch.Core.Commands.Owners;

public class UpdateOwnerCommand : IRequest
{
    public UpdateOwnerCommand(Guid id, string firstName, string lastName, string address, string email, string phone)
    {
        Id = new OwnerId(id);
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Email = email;
        Phone = phone;
    }

    public OwnerId Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}