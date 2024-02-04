using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Shows;

public class UpdateShowCommandHandler : IRequestHandler<UpdateShowCommand>
{
    private readonly IShowRepository _repository;

    public UpdateShowCommandHandler(IShowRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateShowCommand request, CancellationToken cancellationToken)
    {
        var show = new Show(Guid.Empty, request.Name, request.Description, request.Date, request.Website);
        await _repository.Update(request.Id, show);
    }
}