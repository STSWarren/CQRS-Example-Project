using DogSearch.Core.Entities.Owners;
using MediatR;

namespace DogSearch.Core.Commands.Owners;

public class DeleteOwnerCommand : IRequest
{
    public DeleteOwnerCommand(Guid id)
    {
        Id = new OwnerId(id);
    }

    public OwnerId Id { get; set; }
}
