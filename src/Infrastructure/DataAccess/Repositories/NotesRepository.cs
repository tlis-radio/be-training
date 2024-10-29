using Application.Features.Repositories;
using Application.Model;

namespace Infrastructure.DataAccess.Repositories;

public class NotesRepository(ApplicationDbContext dbContext) : 
    DefaultRepository<Note>(dbContext), INotesRepository
{
    public override Task Create(Note entity, CancellationToken cancellationToken = default)
    {
        entity.Created = DateTime.UtcNow;
        
        return base.Create(entity, cancellationToken);
    }

    public override Task Update(Note entity, CancellationToken cancellationToken = default)
    {
        entity.Updated = DateTime.UtcNow;

        return base.Update(entity, cancellationToken);
    }
}