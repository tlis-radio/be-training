using Application.Features.Notes.Projections.Repositories;
using Core.Messaging;
using Domain.Model.Notes;

namespace Application.Features.Notes.Queries.Get;

public class ExistQueryHandler(NoteProjectionsUnitOfWork unitOfWork) : IQueryHandler<ExistQuery, bool>
{
    public Task<bool> Handle(ExistQuery request, CancellationToken cancellationToken)
        => Task.FromResult(unitOfWork.NoteProjections.Query().Any(projection => projection.Id == request.NoteId.Value));
}