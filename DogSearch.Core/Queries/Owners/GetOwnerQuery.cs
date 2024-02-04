using DogSearch.Core.Entities.Owners;
using MediatR;

namespace DogSearch.Core.Queries.Owners;

public class GetOwnerQuery : IRequest<Owner>
{
    public GetOwnerQuery(Guid id)
    {
        Id = new OwnerId(id);
    }

    public OwnerId Id { get; set; }
}