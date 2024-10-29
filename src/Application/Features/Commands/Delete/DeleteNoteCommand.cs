using Core.Messaging;

namespace Application.Features.Commands.Delete;

public record DeleteNoteCommand(Guid NoteId) : ICommand;