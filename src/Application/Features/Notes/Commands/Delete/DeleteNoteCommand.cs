using Core.Messaging;
using Domain.Model.Notes;

namespace Application.Features.Notes.Commands.Delete;

public record DeleteNoteCommand(NoteId NoteId) : ICommand;