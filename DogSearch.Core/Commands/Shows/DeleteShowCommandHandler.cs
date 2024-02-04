using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Shows;

public class DeleteShowCommandHandler : IRequestHandler<DeleteShowCommand>
{
    private readonly IShowRepository _repository;

    public DeleteShowCommandHandler(IShowRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(DeleteShowCommand request, CancellationToken cancellationToken)
    {
        return _repository.Delete(request.Id);
    }
}