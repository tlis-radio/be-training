

using Core.Domain.Model;
using Domain.Model.Notes;
using Domain.Model.Notes.Events;

namespace Infrastructure.DataAccess.Notes;

public class NotesRepository(IEventStore eventStore) : INotesRepository
{
    public async Task<Note> GetById(NoteId identity, CancellationToken cancellationToken = default)
    {
        List<IEvent<NoteId>> events = await eventStore.RetrieveEvents(identity, cancellationToken);
        if (events.Count == 0)
            throw new InvalidOperationException("There is no such entity");
        
        var note = new Note();
        note.Evolve(events);
        return note;
    }

    public async Task<bool> Exists(NoteId identity, CancellationToken cancellationToken = default)
    {
        List<IEvent<NoteId>> events = await eventStore.RetrieveEvents(identity, cancellationToken);
        return events.Count != 0;
    }

    public async Task<DateTime> Created(NoteId identity, CancellationToken cancellationToken)
    {
        List<IEvent<NoteId>> events = await eventStore.RetrieveEvents(identity, cancellationToken);
        return events.First().OccuredOn;
    }

    public async Task<DateTime> Updated(NoteId identity, CancellationToken cancellationToken)
    {
        List<IEvent<NoteId>> events = await eventStore.RetrieveEvents(identity, cancellationToken);
        return events.Last().OccuredOn;
    }
}