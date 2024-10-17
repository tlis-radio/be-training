using Infrastructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories;

public class NotesRepository(ApplicationDbContext dbContext)
{
    public IQueryable<Note> Query() => dbContext.Notes.AsQueryable().AsNoTracking();
    
    public async Task<int> Create(Note note, CancellationToken cancellationToken = default)
    {
        note.Created = DateTime.UtcNow;
        note.Updated = DateTime.UtcNow;
        
        await dbContext.Notes.AddAsync(note, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return note.Id;
    }

    public async Task<Note?> Read(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Notes.FindAsync([id], cancellationToken);
    }

    public async Task Update(Note note, CancellationToken cancellationToken = default)
    {
        note.Updated = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Note note, CancellationToken cancellationToken = default)
    {
        dbContext.Notes.Remove(note);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}