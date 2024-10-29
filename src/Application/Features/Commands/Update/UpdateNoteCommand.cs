using Core.Messaging;

namespace Application.Features.Commands.Update;

public record UpdateNoteCommand(Guid NoteId, string? Title, string? Text) : ICommand;