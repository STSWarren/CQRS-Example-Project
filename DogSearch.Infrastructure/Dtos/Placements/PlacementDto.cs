namespace DogSearch.Infrastructure.Dtos.Placements;

public class PlacementDto : BaseDto
{
    public string DogId { get; init; }
    public string ShowId { get; init; }
    public string Category { get; init; }
    public int Place { get; init; }
}