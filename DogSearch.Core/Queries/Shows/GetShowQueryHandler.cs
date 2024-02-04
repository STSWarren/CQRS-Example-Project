using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Shows;

public class GetShowQueryHandler : IRequestHandler<GetShowQuery, Show>
{
    private readonly IShowRepository _repository;

    public GetShowQueryHandler(IShowRepository repository)
    {
        _repository = repository;
    }

    public Task<Show> Handle(GetShowQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetById(request.Id);
    }
}