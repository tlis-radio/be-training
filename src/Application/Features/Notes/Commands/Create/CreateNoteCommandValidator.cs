using FluentValidation;

namespace Application.Features.Notes.Commands.Create;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(command => command.Title).NotEmpty()
            .MaximumLength(50);
        RuleFor(command => command.Text).NotEmpty();
    }
}