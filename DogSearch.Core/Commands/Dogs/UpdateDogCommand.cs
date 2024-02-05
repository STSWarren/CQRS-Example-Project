using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Owners;
using MediatR;

namespace DogSearch.Core.Commands.Dogs;

public class UpdateDogCommand : IRequest
{
    public UpdateDogCommand(Guid id, string name, string breed, Guid ownerId, Size? size)
    {
        Id = new DogId(id);
        Name = name;
        Breed = breed;
        OwnerId = new OwnerId(ownerId);
        Size = size;
    }
    public DogId Id { get; set; }
    public string Name { get; init; }
    public string Breed { get; init; }
    public OwnerId OwnerId { get; init; }
    public Size? Size { get; init; }
}