namespace DogSearch.Infrastructure.Dtos.Dogs;

public class DogDto : BaseDto
{
    public string Name { get; init; }
    public string Breed { get; init; }
    public string OwnerId { get; init; }
    public string Size { get; init; }
}