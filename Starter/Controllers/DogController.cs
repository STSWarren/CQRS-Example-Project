using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace DogSearch.Application.Controllers;

[Route("api/dog")]
[ApiController]
public class DogController : ControllerBase
{
    private readonly IMediator _mediator;

    public DogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        return NotFound();
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Add(Guid id)
    {
        return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult> Update(Guid id)
    {
        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        return NotFound();
    }
}
