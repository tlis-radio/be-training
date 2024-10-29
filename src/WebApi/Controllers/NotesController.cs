using Application.Features.Commands.Create;
using Application.Features.Commands.Delete;
using Application.Features.Commands.Update;
using Application.Features.Queries.Get;
using Application.Features.Queries.List;
using Application.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class NotesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteDto createNoteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Guid noteId = await mediator.Send(new CreateNoteCommand(createNoteDto.Title, createNoteDto.Text),
            HttpContext.RequestAborted);

        string uri = Url.Action("GetById", new { id = noteId })!;

        return Created(uri, new { Id = noteId });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        if (!await mediator.Send(new ExistQuery(id), HttpContext.RequestAborted))
            return NotFound();

        Note note = await mediator.Send(new GetByIdQuery(id), HttpContext.RequestAborted);

        return Ok(note);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Note> note = await mediator.Send(new ListAllNotes(), HttpContext.RequestAborted);

        return Ok(note);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateNoteDto updateNoteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!await mediator.Send(new ExistQuery(id), HttpContext.RequestAborted))
            return NotFound();

        await mediator.Send(new UpdateNoteCommand(id, updateNoteDto.Title, updateNoteDto.Text),
            HttpContext.RequestAborted);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        if (!await mediator.Send(new ExistQuery(id), HttpContext.RequestAborted))
            return NotFound();

        await mediator.Send(new DeleteNoteCommand(id), HttpContext.RequestAborted);

        return NoContent();
    }
}