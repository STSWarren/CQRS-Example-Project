using DogSearch.Core.Entities.Owners;

namespace DogSearch.Core.Entities.Dog;

public class Dog
{
    public Dog(Guid id, string name, string breed, Guid ownerId, Size? size)
    {
        Id = new DogId(id);
        Name = name;
        Breed = breed;
        OwnerId = new OwnerId(ownerId);
        Size = size;
    }

    public DogId Id { get; init; }
    public string Name { get; init; }
    public string Breed { get; init; }
    public OwnerId OwnerId { get; init; }
    public Size? Size { get; init; }
}