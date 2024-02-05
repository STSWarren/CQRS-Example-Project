namespace DogSearch.Infrastructure.Dtos.Shows;

public class ShowDto:BaseDto
{
    public string name { get; init; }
    public string description { get; init; }
    public DateTimeOffset date { get; init; }
    public string website { get; init; }
}
