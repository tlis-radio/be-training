using Application.Features.Notes.Projections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Notes.Projections.Configurations;

public class NotesConfiguration : IEntityTypeConfiguration<NoteProjection>
{
    public void Configure(EntityTypeBuilder<NoteProjection> builder)
    {
        builder.HasKey(note => note.Id);
        builder.Property(note => note.Id)
            .ValueGeneratedNever();
        builder.Property(note => note.Title)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(note => note.Text)
            .IsRequired();
    }
}