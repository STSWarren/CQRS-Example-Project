using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Shows;

public class ListShowsByIdQueryHandler : IRequestHandler<ListShowsByIdQuery, IEnumerable<Show>>
{
    private readonly IShowRepository _repository;

    public ListShowsByIdQueryHandler(IShowRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Show>> Handle(ListShowsByIdQuery request, CancellationToken cancellationToken)
    {
        return _repository.ListByIds(request.Ids);
    }
}