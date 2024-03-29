﻿namespace DogSearch.Api.Dtos.Owners;

public class UpdateOwnerRequestDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Address { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
}