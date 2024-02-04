using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Shows;

public class GetAllShowsQueryHandler : IRequestHandler<GetAllShowsQuery, IEnumerable<Show>>
{
    private readonly IShowRepository _repository;

    public GetAllShowsQueryHandler(IShowRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Show>> Handle(GetAllShowsQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAll();
    }
}