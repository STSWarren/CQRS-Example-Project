using System.ComponentModel.DataAnnotations;

namespace DogSearch.Api.Dtos;

public class CreateDogRequestDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Breed { get; set; }
    [Required]
    public Guid OwnerId { get; set; }
    [Required]
    public string Size { get; set; }
}
