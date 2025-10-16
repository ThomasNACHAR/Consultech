using Consultech.Business.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Consultech.API.Models;

public sealed class ConsultantInput
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;

    public DateTime StartDate { get; set; } = DateTime.Today;
    [Required]
    public string Email { get; set; } = string.Empty;

    public bool IsAvailable { get; set; } = true;

    public List<int> SkillsId { get; set; } = new();
}
