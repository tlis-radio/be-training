

using Core.Domain.Model;
using Domain.Model.Notes;

namespace Infrastructure.DataAccess.Notes;

public class NotesRepository(IEventStore eventStore) : INotesRepository
{
    public async Task<Note> GetById(NoteId identity, CancellationToken cancellationToken = default)
    {
        var note = new Note();
        List<IEvent<NoteId>> events = await eventStore.RetrieveEvents(identity, cancellationToken);
        note.Evolve(events);
        return note;
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