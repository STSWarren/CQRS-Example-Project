using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Commands.Shows;

public class CreateShowCommand : IRequest<ShowId>
{
    public CreateShowCommand(string name, string description, DateTimeOffset date, string website)
    {
        Name = name;
        Description = description;
        Date = date;
        Website = website;
    }

    public string Name { get; init; }
    public string Description { get; init; }
    public DateTimeOffset Date { get; init; }
    public string Website { get; init; }
}