using Core.Messaging;
using Domain.Model.Notes;

namespace Application.Features.Notes.Commands.Create;

public record CreateNoteCommand(string Title, string Text) : ICommand<NoteId>;