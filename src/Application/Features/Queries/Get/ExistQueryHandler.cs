using Core.Messaging;

namespace Application.Features.Queries.Get;

public class ExistQueryHandler(UnitOfWork unitOfWork) : IQueryHandler<ExistQuery, bool>
{
    public Task<bool> Handle(ExistQuery request, CancellationToken cancellationToken)
        => Task.FromResult(unitOfWork.Notes.Query().Any(projection => projection.Id == request.NoteId));
}