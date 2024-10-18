using Application.Features;
using Application.Features.Notes;
using Application.Features.Notes.Projections;
using Core.Domain.Model;
using Domain.Model.Notes;
using Domain.Model.Notes.Events;

namespace Application.Handlers.Notes;

public class NotesProjectionsUpdater(NoteProjectionsUnitOfWork unitOfWork, INotesRepository notesRepository) :
    IEventHandler<NewNoteCreated, NoteId>,
    IEventHandler<NoteTextChanged, NoteId>,
    IEventHandler<NoteTitleChanged, NoteId>
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

    private async Task<NoteProjection> RetrieveById(NoteId noteId, CancellationToken cancellationToken)
    {
        NoteProjection? noteProjection = await unitOfWork.NoteProjections.Read([noteId.Value], cancellationToken);

        if (noteProjection is not null)
            return noteProjection;

        Note note = await notesRepository.GetById(noteId, cancellationToken);

        noteProjection = new NoteProjection
        {
            Id = note.Identity.Value,
            Title = note.Title,
            Text = note.Text,
            Created = await notesRepository.Created(noteId, cancellationToken),
            Updated = await notesRepository.Updated(noteId, cancellationToken)
        };

        await unitOfWork.NoteProjections.Create(noteProjection, cancellationToken);

        return noteProjection;
    }
}