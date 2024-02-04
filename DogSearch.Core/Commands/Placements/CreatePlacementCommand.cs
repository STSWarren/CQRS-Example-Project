using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Commands.Placements;

public class CreatePlacementCommand : IRequest
{
    public CreatePlacementCommand(DogId dogId, ShowId showId, string category, int place)
    {
        DogId = dogId;
        ShowId = showId;
        Category = category;
        Place = place;
    }

    public DogId DogId { get; init; }
    public ShowId ShowId { get; init; }
    public string Category { get; init; }
    public int Place { get; init; }
}