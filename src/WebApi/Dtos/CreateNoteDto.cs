using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos;

public class CreateNoteDto
{
    [Required] [StringLength(50)] public string Titile { get; set; } = null!;

    [Required] public string Text { get; set; } = null!;
}