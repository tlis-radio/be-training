using Core.Messaging;
using Domain.Model.Notes;

namespace Application.Features.Notes.Commands.Update;

public record UpdateNoteCommand(NoteId NoteId, string? Title, string? Text) : ICommand;