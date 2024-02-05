using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Dogs;

public class UpdateDogCommandHandler : IRequestHandler<UpdateDogCommand>
{
    private readonly IDogRepository _repository;

    public UpdateDogCommandHandler(IDogRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(UpdateDogCommand request, CancellationToken cancellationToken)
    {
        return _repository.Update(
            request.Id,
            new Dog(request.Id.Value,
            request.Name,
            request.Breed,
            request.OwnerId.Value,
            request.Size));
    }
}