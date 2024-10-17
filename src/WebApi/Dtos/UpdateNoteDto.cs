using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Dtos;

public class UpdateNoteDto
{
    [StringLength(50)] public string? Title { get; set; }
    
    public string? Text { get; set; }
}