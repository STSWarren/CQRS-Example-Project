using System.ComponentModel.DataAnnotations;

namespace DogSearch.Api.Dtos;

public class UpdateDogRequestDto
{
    public string? Name { get; set; }
    public string? Breed { get; set; }
    public Guid? OwnerId { get; set; }
    public string? Size { get; set; }
}
