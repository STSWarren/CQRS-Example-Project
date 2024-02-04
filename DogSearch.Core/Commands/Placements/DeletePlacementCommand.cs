using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Commands.Placements;

public class DeletePlacementCommand : IRequest
{
    public DeletePlacementCommand(DogId dogId, ShowId showId, string category)
    {
        DogId = dogId;
        ShowId = showId;
        Category = category;
    }

    public DogId DogId { get; init; }
    public ShowId ShowId { get; init; }
    public string Category { get; init; }
}