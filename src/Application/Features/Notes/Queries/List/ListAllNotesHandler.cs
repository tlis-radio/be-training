using Application.Features.Notes.Projections;
using Core.Messaging;

namespace Application.Features.Notes.Queries.List;

public class ListAllNotesHandler(NoteProjectionsUnitOfWork unitOfWork) : IQueryHandler<ListAllNotes, List<NoteProjection>>
{
    public Task<List<NoteProjection>> Handle(ListAllNotes request, CancellationToken cancellationToken) => 
        Task.FromResult(unitOfWork.NoteProjections.Query().ToList());
}