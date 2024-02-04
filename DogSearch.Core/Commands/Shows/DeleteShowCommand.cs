using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Commands.Shows;

public class DeleteShowCommand : IRequest
{
    public DeleteShowCommand(ShowId id)
    {
        Id = id;
    }
    public ShowId Id { get; init; }
}
