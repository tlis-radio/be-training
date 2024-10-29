using Application.Features.Queries.Get;
using Core.Messaging;
using MediatR;

namespace Application.Features.Commands.Delete;

public class DeleteNoteCommandHandler(UnitOfWork unitOfWork, ISender sender) : ICommandHandler<DeleteNoteCommand>
{
    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        if (!await sender.Send(new ExistQuery(request.NoteId), cancellationToken))
            throw new InvalidOperationException("An operation with a non existing entity");
        
        await unitOfWork.Notes.Delete([request.NoteId], cancellationToken);
    }
}