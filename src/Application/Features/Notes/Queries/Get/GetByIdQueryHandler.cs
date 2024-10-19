using Application.Features.Notes.Projections;
using Core.Messaging;

namespace Application.Features.Notes.Queries.Get;

public class GetByIdQueryHandler(NoteProjectionsUnitOfWork unitOfWork) : IQueryHandler<GetByIdQuery, NoteProjection>
{
    public Task<NoteProjection> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            unitOfWork.NoteProjections.Query().First(projection => projection.Id == request.NoteId.Value)
        );
    }
}