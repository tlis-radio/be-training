using Application.Features.Notes.Queries.Get;
using Core.Messaging;
using Domain.Model.Notes.Events;
using MediatR;

namespace Application.Features.Notes.Commands.Delete;

public class DeleteNoteCommandHandler(IPublisher publisher, ISender sender) : ICommandHandler<DeleteNoteCommand>
{
    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        if (!await sender.Send(new ExistQuery(request.NoteId), cancellationToken))
            throw new InvalidOperationException("An operation with a deleted entity");
        
        await publisher.Publish(new NoteDeleted
        {
            AggregateRootId = request.NoteId,
            OccuredOn = DateTime.UtcNow,
        }, cancellationToken);
    }
}