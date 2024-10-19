using Core.Messaging;
using Domain.Model.Notes.Events;
using MediatR;

namespace Application.Features.Notes.Commands.Delete;

public class DeleteNoteCommandHandler(IPublisher publisher) : ICommandHandler<DeleteNoteCommand>
{
    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        //TODO, not sure how to exactly delete entity in Event Sourcing
        await publisher.Publish(new NoteDeleted
        {
            AggregateRootId = request.NoteId,
            OccuredOn = DateTime.UtcNow,
        }, cancellationToken);
    }
}