using DogSearch.Core.Entities.Owners;
using MediatR;

namespace DogSearch.Core.Queries.Owners;

public class GetOwnerQuery : IRequest<Owner>
{
    public GetOwnerQuery(OwnerId id)
    {
        Id = id;
    }

    public OwnerId Id { get; set; }
}