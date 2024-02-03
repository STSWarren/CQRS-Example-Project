using DogSearch.Core.Entities.Dog;
using MediatR;

namespace DogSearch.Core.Commands.Dogs;

public class CreateDogCommand : IRequest<DogId>
{
    public CreateDogCommand(string name, string breed, Guid ownerId, Size size)
    {
        Name = name;
        Breed = breed;
        OwnerId = ownerId;
        Size = size;
    }

    public string Name { get; init; }
    public string Breed { get; init; }
    public Guid OwnerId { get; init; }
    public Size Size { get; init; }
}