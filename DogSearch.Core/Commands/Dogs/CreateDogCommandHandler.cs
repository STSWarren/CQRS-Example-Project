using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Dogs;

public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, DogId>
{
    private readonly IDogRepository _repository;

    public CreateDogCommandHandler(IDogRepository repository)
    {
        _repository = repository;
    }

    public async Task<DogId> Handle(CreateDogCommand request, CancellationToken cancellationToken)
    {
        var dog = new Dog(Guid.NewGuid(), request.Name, request.Breed, request.OwnerId, request.Size);
        await _repository.Create(dog);
        return dog.Id;
    }
}