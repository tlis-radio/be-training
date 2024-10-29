using Application.Features.Repositories;

namespace Application.Features;

public class UnitOfWork(INotesRepository notesRepository)
{
    public INotesRepository Notes { get; } = notesRepository;
}