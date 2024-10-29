using Application.Features.Queries.Get;
using Application.Model;
using Core.Messaging;
using MediatR;

namespace Application.Features.Commands.Update;

public class UpdateNoteCommandHandler(UnitOfWork unitOfWork, ISender sender) : ICommandHandler<UpdateNoteCommand>
{
    public async Task Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        if (!await sender.Send(new ExistQuery(request.NoteId), cancellationToken))
            throw new InvalidOperationException("An operation with a non existing entity");

        Note note = (await unitOfWork.Notes.Read([request.NoteId], cancellationToken))!;

        if (request is { Title: not null })
            note.Title = request.Title;
        if (request is { Text: not null })
            note.Text = request.Text;

        await unitOfWork.Notes.Update(note, cancellationToken);
    }
}