using Core.Messaging;
using Domain.Model.Notes;
using MediatR;

namespace Application.Features.Notes.Commands.Create;

public class CreateNoteCommandHandler(IPublisher publisher) : ICommandHandler<CreateNoteCommand, NoteId>
{
    public async Task<NoteId> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var noteId = new NoteId(Guid.NewGuid());
        var note = new Note(noteId, request.Title, request.Text);

        await note.PublishEvents(publisher);

        return noteId;
    }
}