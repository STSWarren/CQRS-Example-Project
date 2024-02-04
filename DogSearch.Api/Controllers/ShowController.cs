using DogSearch.Api.Dtos.Shows;
using DogSearch.Core.Commands.Shows;
using DogSearch.Core.Queries.Shows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DogSearch.Api.Controllers;

[Route("api/show")]
[ApiController]
public class ShowController : Controller
{
    private readonly IMediator _mediator;

    public ShowController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var query = new GetShowQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var query = new GetAllShowsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateShowRequestDto dto)
    {
        var command = new CreateShowCommand(dto.Name, dto.Description, DateTimeOffset.Parse(dto.Date), dto.Website);
        var showId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id = showId.Value });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateShowRequestDto dto)
    {
        var date = new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero);
        if (!dto.Date.Equals(null))
        {
            date = DateTimeOffset.Parse(dto.Date);
        }
        var command = new UpdateShowCommand(
            id,
            dto.Name ?? string.Empty,
            dto.Description ?? string.Empty,
            date,
            dto.Website ?? string.Empty);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteShowCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
