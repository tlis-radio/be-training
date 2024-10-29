using Application.Model;
using Core.Messaging;

namespace Application.Features.Queries.List;

public class ListAllNotesHandler(UnitOfWork unitOfWork) : IQueryHandler<ListAllNotes, List<Note>>
{
    public Task<List<Note>> Handle(ListAllNotes request, CancellationToken cancellationToken) => 
        Task.FromResult(unitOfWork.Notes.Query().ToList());
}