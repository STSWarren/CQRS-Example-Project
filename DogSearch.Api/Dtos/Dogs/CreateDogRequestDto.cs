using System.ComponentModel.DataAnnotations;

namespace DogSearch.Api.Dtos.Dogs;

public class CreateDogRequestDto
{
    [Required]
    public string Name { get; init; }
    [Required]
    public string Breed { get; init; }
    [Required]
    public Guid OwnerId { get; init; }
    [Required]
    public string Size { get; init; }
}
