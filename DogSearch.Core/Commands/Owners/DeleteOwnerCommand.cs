using DogSearch.Core.Entities.Owners;
using MediatR;

namespace DogSearch.Core.Commands.Owners;

public class DeleteOwnerCommand : IRequest
{
    public DeleteOwnerCommand(OwnerId id)
    {
        Id = id;
    }

    public OwnerId Id { get; set; }
}
