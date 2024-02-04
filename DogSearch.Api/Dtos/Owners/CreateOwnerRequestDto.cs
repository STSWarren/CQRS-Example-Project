using System.ComponentModel.DataAnnotations;

namespace DogSearch.Api.Dtos.Owners;

public class CreateOwnerRequestDto
{
    [Required]
    public string FirstName { get; init; }
    [Required]
    public string LastName { get; init; }
    [Required]
    public string Address { get; init; }
    [Required]
    public string Email { get; init; }
    [Required]
    public string Phone { get; init; }
}
