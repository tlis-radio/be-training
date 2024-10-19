using FluentValidation;

namespace Application.Features.Notes.Commands.Update;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(command => command.Title).MaximumLength(50);
    }
}