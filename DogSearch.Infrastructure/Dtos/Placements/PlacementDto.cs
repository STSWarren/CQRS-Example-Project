namespace DogSearch.Infrastructure.Dtos.Placements;

public class PlacementDto : BaseDto
{
    public string dog_id { get; init; }
    public string show_id { get; init; }
    public string category { get; init; }
    public int place { get; init; }
}