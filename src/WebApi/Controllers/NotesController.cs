using Infrastructure.DataAccess.Models;
using Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class NotesController(NotesRepository notesRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteDto createNoteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var note = new Note
        {
            Title = createNoteDto.Titile,
            Text = createNoteDto.Text
        };

        int noteId = await notesRepository.Create(note, HttpContext.RequestAborted);

        string uri = Url.Action("GetById", new { id = noteId })!;

        return Created(uri, new { Id = noteId });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        Note? note = await notesRepository.Read(id, HttpContext.RequestAborted);

        if (note is null)
            return NotFound();

        return Ok(note);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Note> notes = await notesRepository.Query().ToListAsync();

        return Ok(notes);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNoteDto updateNoteDto)
    {
        Note? note = await notesRepository.Read(id, HttpContext.RequestAborted);
        
        if (note is null)
            return NotFound();

        if (updateNoteDto is { Title: not null })
            note.Title = updateNoteDto.Title;

        if (updateNoteDto is { Text: not null })
            note.Text = updateNoteDto.Text;

        await notesRepository.Update(note, HttpContext.RequestAborted);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        Note? note = await notesRepository.Read(id, HttpContext.RequestAborted);
        
        if (note is null)
            return NotFound();

        await notesRepository.Delete(note, HttpContext.RequestAborted);

        return NoContent();
    }
}