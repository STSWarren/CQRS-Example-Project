namespace DogSearch.Infrastructure.Dtos.Shows;

public class ShowDto:BaseDto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTimeOffset Date { get; init; }
    public string Website { get; init; }
}
