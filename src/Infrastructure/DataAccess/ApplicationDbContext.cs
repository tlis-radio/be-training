using Application.Model;
using Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Note> NoteProjections { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NotesConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}