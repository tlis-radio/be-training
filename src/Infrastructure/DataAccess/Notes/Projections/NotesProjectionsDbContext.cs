using Application.Features.Notes.Projections;
using Infrastructure.DataAccess.Notes.Projections.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Notes.Projections;

public class NotesProjectionsDbContext(DbContextOptions<NotesProjectionsDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<NoteProjection> NoteProjections { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NotesConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}