using Core.Messaging;

namespace Application.Features.Commands.Create;

public record CreateNoteCommand(string Title, string Text) : ICommand<Guid>;