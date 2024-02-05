namespace DogSearch.Infrastructure.Dtos.Dogs;

public class DogDto : BaseDto
{
    public string name { get; init; }
    public string breed { get; init; }
    public string owner_id { get; init; }
    public string size { get; init; }
}