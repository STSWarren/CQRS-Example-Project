using System.ComponentModel.DataAnnotations;

namespace DogSearch.Api.Dtos.Shows;

public class CreateShowRequestDto
{
    [Required]
    public string Name { get; init; }
    [Required]
    public string Description { get; init; }
    [Required]
    public string Date { get; init; }
    [Required]
    public string Website { get; init; }
}
