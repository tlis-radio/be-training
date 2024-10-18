using Core.Domain.Model;
using Domain.Model.Notes;

namespace Application.Handlers.Notes;

public class NoteEventsHandler<TEvent>(IEventStore eventStore) : IEventHandler<TEvent, NoteId>
    where TEvent : IEvent<NoteId>
{
    public async Task Handle(TEvent notification, CancellationToken cancellationToken) =>
        await eventStore.AppendEvent(notification, cancellationToken);
}