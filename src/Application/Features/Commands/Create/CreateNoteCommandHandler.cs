using Application.Model;
using Core.Messaging;

namespace Application.Features.Commands.Create;

public class CreateNoteCommandHandler(UnitOfWork unitOfWork) : ICommandHandler<CreateNoteCommand, Guid>
{
    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note
        {
            Id = Guid.NewGuid(),
            Text = request.Text,
            Title = request.Title
        };

        await unitOfWork.Notes.Create(note, cancellationToken);

        return note.Id;
    }
}