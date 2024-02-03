using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands;

public class DeleteDogCommandHandler:IRequestHandler<DeleteDogCommand>
{
    private readonly IDogRepository _repository;

    public DeleteDogCommandHandler(IDogRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(DeleteDogCommand request, CancellationToken cancellationToken)
    {
        return _repository.Delete(request.Id);
    }
}