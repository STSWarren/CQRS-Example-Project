using DogSearch.Api.Dtos.Owners;
using DogSearch.Core.Commands.Owners;
using DogSearch.Core.Queries.Owners;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DogSearch.Api.Controllers;

[Route("api/owner")]
[ApiController]
public class OwnerController : Controller
{
    private readonly IMediator _mediator;

    public OwnerController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var query = new GetOwnerQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var query = new GetAllOwnersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateOwnerRequestDto dto)
    {
        var command = new CreateOwnerCommand(dto.FirstName, dto.LastName, dto.Address, dto.Email, dto.Phone);
        var ownerId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id = ownerId.Value });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateOwnerRequestDto dto)
    {
        var command = new UpdateOwnerCommand(
            id, 
            dto.FirstName ?? string.Empty, 
            dto.LastName ?? string.Empty, 
            dto.Address ?? string.Empty,
            dto.Email ?? string.Empty, 
            dto.Phone ?? string.Empty);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteOwnerCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
