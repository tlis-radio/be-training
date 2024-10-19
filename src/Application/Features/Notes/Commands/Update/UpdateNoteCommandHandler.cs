using Core.Messaging;
using Domain.Model.Notes;
using MediatR;

namespace Application.Features.Notes.Commands.Update;

public class UpdateNoteCommandHandler(INotesRepository notesRepository, IPublisher publisher) : ICommandHandler<UpdateNoteCommand>
{
    public async Task Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        Note note = await notesRepository.GetById(request.NoteId, cancellationToken);
        
        if (request is { Title: not null })
            note.ChangeTitle(request.Title);
        if (request is { Text: not null })
            note.ChangeText(request.Text);

        await note.PublishEvents(publisher);
    }
}