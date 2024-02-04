using System.ComponentModel.DataAnnotations;

namespace DogSearch.Api.Dtos.Placements;

public class CreatePlacementRequestDto
{
    [Required]
    public Guid DogId { get; init; }
    [Required]
    public Guid ShowId { get; init; }
    [Required]
    public string Category { get; init; }
    [Required]
    public int Place { get; init; }
}
