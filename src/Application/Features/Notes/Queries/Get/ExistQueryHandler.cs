using Core.Messaging;
using Domain.Model.Notes;

namespace Application.Features.Notes.Queries.Get;

public class ExistQueryHandler(INotesRepository notesRepository) : IQueryHandler<ExistQuery, bool>
{
    public async Task<bool> Handle(ExistQuery request, CancellationToken cancellationToken)
        => await notesRepository.Exists(request.NoteId, cancellationToken);
}