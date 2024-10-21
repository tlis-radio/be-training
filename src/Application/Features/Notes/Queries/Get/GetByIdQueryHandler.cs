using Application.Features.Notes.Projections;
using Core.Messaging;
using MediatR;

namespace Application.Features.Notes.Queries.Get;

public class GetByIdQueryHandler(NoteProjectionsUnitOfWork unitOfWork, ISender sender) : IQueryHandler<GetByIdQuery, NoteProjection>
{
    public async Task<NoteProjection> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        if (!await sender.Send(new ExistQuery(request.NoteId), cancellationToken))
            throw new InvalidOperationException("An operation with a deleted entity");

        return unitOfWork.NoteProjections.Query().First(projection => projection.Id == request.NoteId.Value);
    }
}