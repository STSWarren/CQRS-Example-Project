namespace DogSearch.Infrastructure.Dtos.Owners;

public class OwnerDto :BaseDto
{
    public string first_name { get; init; }
    public string last_name { get; init; }
    public string address { get; init; }
    public string email { get; init; }
    public string phone { get; init; }
}