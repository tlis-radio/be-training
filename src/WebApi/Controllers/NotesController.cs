using Application.Features.Notes.Commands.Create;
using Application.Features.Notes.Commands.Delete;
using Application.Features.Notes.Commands.Update;
using Application.Features.Notes.Projections;
using Application.Features.Notes.Queries.Get;
using Domain.Model.Notes;
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

        NoteId noteId = await mediator.Send(new CreateNoteCommand(createNoteDto.Title, createNoteDto.Text),
            HttpContext.RequestAborted);

        string uri = Url.Action("GetById", new { id = noteId.Value })!;

        return Created(uri, new { Id = noteId });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var noteId = new NoteId(id);
        bool exist = await mediator.Send(new ExistQuery(noteId), HttpContext.RequestAborted);

        if (!exist)
            return NotFound();

        NoteProjection note = await mediator.Send(new GetByIdQuery(noteId), HttpContext.RequestAborted);

        return Ok(note);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateNoteDto updateNoteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var noteId = new NoteId(id);
        bool exist = await mediator.Send(new ExistQuery(noteId), HttpContext.RequestAborted);
        
        if (!exist)
            return NotFound();

        await mediator.Send(new UpdateNoteCommand(noteId, updateNoteDto.Title, updateNoteDto.Text),
            HttpContext.RequestAborted);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var noteId = new NoteId(id);
        bool exist = await mediator.Send(new ExistQuery(noteId), HttpContext.RequestAborted);
        
        if (!exist)
            return NotFound();

        await mediator.Send(new DeleteNoteCommand(noteId), HttpContext.RequestAborted);

        return NoContent();
    }
}