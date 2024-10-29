using Application.Model;
using Core.Messaging;
using MediatR;

namespace Application.Features.Queries.Get;

public class GetByIdQueryHandler(UnitOfWork unitOfWork, ISender sender) : IQueryHandler<GetByIdQuery, Note>
{
    public async Task<Note> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        if (!await sender.Send(new ExistQuery(request.NoteId), cancellationToken))
            throw new InvalidOperationException("An operation with a deleted entity");

        return unitOfWork.Notes.Query().First(projection => projection.Id == request.NoteId);
    }
}