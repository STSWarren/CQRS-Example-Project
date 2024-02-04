using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Commands.Shows;

public class DeleteShowCommand : IRequest
{
    public DeleteShowCommand(Guid id)
    {
        Id = new ShowId(id);
    }
    public ShowId Id { get; init; }
}
