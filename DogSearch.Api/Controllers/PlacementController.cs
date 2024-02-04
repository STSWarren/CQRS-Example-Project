using DogSearch.Api.Dtos.Placements;
using DogSearch.Core.Commands.Placements;
using DogSearch.Core.Queries.Dogs;
using DogSearch.Core.Queries.Placements;
using DogSearch.Core.Queries.Shows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DogSearch.Api.Controllers;

[Route("api/placement")]
[ApiController]
public class PlacementController : Controller
{
    private readonly IMediator _mediator;

    public PlacementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> Get([FromQuery] Guid? showId, [FromQuery] Guid? dogId, [FromQuery] string category)
    {
        var query = new ListPlacementsQuery(dogId ?? Guid.Empty, showId ?? Guid.Empty, category ?? string.Empty);
        var result = await _mediator.Send(query);
        var listShowsByIdsQuery = new ListShowsByIdQuery(result.Select(x=>x.ShowId));
        var shows = await _mediator.Send(listShowsByIdsQuery);
        var listDogsByIdsQuery = new ListDogsByIdQuery(result.Select(x => x.DogId));
        var dogs = await _mediator.Send(listShowsByIdsQuery);
        var response = result.Select(x=>new GetPlacementResponseDto { 
            ShowName = shows.First(y=>y.Id.Value.Equals(x.ShowId.Value)).Name,
            DogName = dogs.First(y=>y.Id.Value.Equals(x.DogId.Value)).Name,
            Category = x.Category,
            Place = x.Place,
        });
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var query = new GetAllPlacementsQuery();
        var result = await _mediator.Send(query); var listShowsByIdsQuery = new ListShowsByIdQuery(result.Select(x => x.ShowId));
        var shows = await _mediator.Send(listShowsByIdsQuery);
        var listDogsByIdsQuery = new ListDogsByIdQuery(result.Select(x => x.DogId));
        var dogs = await _mediator.Send(listShowsByIdsQuery);
        var response = result.Select(x => new GetPlacementResponseDto
        {
            ShowName = shows.First(y => y.Id.Value.Equals(x.ShowId.Value)).Name,
            DogName = dogs.First(y => y.Id.Value.Equals(x.DogId.Value)).Name,
            Category = x.Category,
            Place = x.Place,
        });
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreatePlacementRequestDto dto)
    {
        var command = new CreatePlacementCommand(dto.DogId, dto.ShowId, dto.Category, dto.Place);
        await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { 
            dogId = dto.DogId, showId = dto.ShowId, 
            category = dto.Category, place = dto.Place });
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdatePlacementRequestDto dto)
    {
        var command = new UpdatePlacementCommand(dto.DogId, dto.ShowId, dto.Category, dto.Place);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] DeletePlacementRequestDto dto)
    {
        var command = new DeletePlacementCommand(dto.DogId, dto.ShowId, dto.Category);
        await _mediator.Send(command);
        return NoContent();
    }
}
