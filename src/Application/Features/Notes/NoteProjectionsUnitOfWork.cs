using Application.Features.Notes.Projections.Repositories;

namespace Application.Features.Notes;

public class NoteProjectionsUnitOfWork(INoteProjectionsRepository noteProjectionsRepository)
{
    public INoteProjectionsRepository NoteProjections { get; } = noteProjectionsRepository;
}