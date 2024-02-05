using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Shows;

namespace DogSearch.Core.Entities.Placements;

public class Placement
{
    public Placement(Guid id, Guid dogId, Guid showId, string category, int place)
    {
        Id = new PlacementId(id);
        DogId = new DogId(dogId);
        ShowId = new ShowId(showId);
        Category = category;
        Place = place;
    }

    public PlacementId Id { get; init; }
    public DogId DogId { get; init; }
    public ShowId ShowId { get; init; }
    public string Category { get; init; }
    public int Place { get; init; }
}