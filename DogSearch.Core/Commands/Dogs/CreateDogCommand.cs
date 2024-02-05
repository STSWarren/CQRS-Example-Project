using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Owners;
using MediatR;

namespace DogSearch.Core.Commands.Dogs;

public class CreateDogCommand : IRequest<DogId>
{
    public CreateDogCommand(string name, string breed, Guid ownerId, Size size)
    {
        Name = name;
        Breed = breed;
        OwnerId = new OwnerId(ownerId);
        Size = size;
    }

    public string Name { get; init; }
    public string Breed { get; init; }
    public OwnerId OwnerId { get; init; }
    public Size Size { get; init; }
}