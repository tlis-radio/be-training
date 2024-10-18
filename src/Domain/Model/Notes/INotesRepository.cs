using Core.Domain.Model;

namespace Domain.Model.Notes;

public interface INotesRepository : IRepository<Note, NoteId>
{
    public Task<DateTime> Created(NoteId identity, CancellationToken cancellationToken = default);

    public Task<DateTime> Updated(NoteId identity, CancellationToken cancellationToken = default);
}