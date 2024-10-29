using Application.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

public class NotesConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
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