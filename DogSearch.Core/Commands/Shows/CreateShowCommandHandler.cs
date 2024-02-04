using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Shows;

public class CreateShowCommandHandler : IRequestHandler<CreateShowCommand, ShowId>
{
    private readonly IShowRepository _repository;

    public CreateShowCommandHandler(IShowRepository repository)
    {
        _repository = repository;
    }

    public async Task<ShowId> Handle(CreateShowCommand request, CancellationToken cancellationToken)
    {
        var show = new Show(Guid.NewGuid(), request.Name, request.Description, request.Date, request.Website);
        await _repository.Create(show);
        return show.Id;
    }
}