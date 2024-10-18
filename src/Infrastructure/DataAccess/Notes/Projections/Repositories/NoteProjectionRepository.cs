using Application.Features.Notes.Projections;
using Application.Features.Notes.Projections.Repositories;

namespace Infrastructure.DataAccess.Notes.Projections.Repositories;

public class NoteProjectionRepository(NotesProjectionsDbContext dbContext) : 
    DefaultProjectionsRepository<NoteProjection>(dbContext), INoteProjectionsRepository
{
    public override Task Update(NoteProjection projection, CancellationToken cancellationToken = default)
    {
        projection.Updated = DateTime.UtcNow;

        return base.Update(projection, cancellationToken);
    }
}