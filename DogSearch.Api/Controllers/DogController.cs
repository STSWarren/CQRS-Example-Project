using Microsoft.AspNetCore.Mvc;
using MediatR;
using DogSearch.Core.Queries;
using DogSearch.Api.Dtos;
using DogSearch.Core.Commands;
using DogSearch.Core.Entities.Dog;

namespace DogSearch.Application.Controllers;

[Route("api/dog")]
[ApiController]
public class DogController : Controller
{
    private readonly IMediator _mediator;

    public DogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var query = new GetDogQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var query = new GetAllDogsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateDogRequestDto dto)
    {
        var command = new CreateDogCommand(dto.Name, dto.Breed, dto.OwnerId, Enum.Parse<Size>(dto.Size));
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateDogRequestDto dto)
    {
        Size? passedSize = null;
        if (dto.Size != null)
        {
            var sizeLower = dto.Size.ToLower();
            var sizeFormatted = string.Concat(sizeLower[0].ToString().ToUpper(), sizeLower.AsSpan(1));
            passedSize = Enum.Parse<Size>(sizeFormatted);
        }
        var command = new UpdateDogCommand(
            id, 
            dto.Name?? string.Empty, 
            dto.Breed ?? string.Empty, 
            dto.OwnerId ?? Guid.Empty,
            passedSize);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteDogCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
