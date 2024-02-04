﻿namespace DogSearch.Core.Entities.Shows;

public class Show
{
    public Show(Guid id, string name, string description, DateTimeOffset date, string website)
    {
        Id = new ShowId(id);
        Name = name;
        Description = description;
        Website = website;
        Date = date;
        Website = website;
    }
    public ShowId Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTimeOffset Date { get; init; }
    public string Website { get; init; }
}