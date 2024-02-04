namespace DogSearch.Api.Dtos.Dogs;

public class UpdateDogRequestDto
{
    public string? Name { get; init; }
    public string? Breed { get; init; }
    public Guid? OwnerId { get; init; }
    public string? Size { get; init; }
}
