using Application.Features.Notes;
using Application.Features.Notes.Projections;
using Core.Domain.Model;
using Domain.Model.Notes;
using Domain.Model.Notes.Events;
using MediatR;

namespace Application.Handlers.Notes;

public class NotesProjectionsUpdater(NoteProjectionsUnitOfWork unitOfWork) :
    IEventHandler<NewNoteCreated, NoteId>,
    IEventHandler<NoteTextChanged, NoteId>,
    IEventHandler<NoteTitleChanged, NoteId>,
    IEventHandler<NoteDeleted, NoteId>
{
    public async Task Handle(NewNoteCreated newNoteCreated, CancellationToken cancellationToken)
    {
        var note = new NoteProjection
        {
            Id = newNoteCreated.NoteId.Value,
            Title = newNoteCreated.Title,
            Text = newNoteCreated.Text,
            Created = newNoteCreated.OccuredOn,
            Updated = newNoteCreated.OccuredOn
        };
        
        await unitOfWork.NoteProjections.Create(note, cancellationToken);
    }

    public async Task Handle(NoteTextChanged noteTextChanged, CancellationToken cancellationToken)
    {
        NoteProjection noteProjection = await RetrieveById(noteTextChanged.AggregateRootId, cancellationToken);

        noteProjection.Text = noteTextChanged.Text;

        await unitOfWork.NoteProjections.Update(noteProjection, cancellationToken);
    }

    public async Task Handle(NoteTitleChanged noteTitleChanged, CancellationToken cancellationToken)
    {
        NoteProjection noteProjection = await RetrieveById(noteTitleChanged.AggregateRootId, cancellationToken);

        noteProjection.Title = noteTitleChanged.Title;

        await unitOfWork.NoteProjections.Update(noteProjection, cancellationToken);
    }

    public async Task Handle(NoteDeleted noteDeleted, CancellationToken cancellationToken) =>
        await unitOfWork.NoteProjections.Delete([noteDeleted.AggregateRootId.Value], cancellationToken);

    private async Task<NoteProjection> RetrieveById(NoteId noteId, CancellationToken cancellationToken)
    {
        NoteProjection? noteProjection = await unitOfWork.NoteProjections.Read([noteId.Value], cancellationToken);

        if (noteProjection is null)
            throw new InvalidOperationException("Trying to retrieve a projection that has not been created.");

        return noteProjection;
    }
}