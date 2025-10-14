using System.ComponentModel.DataAnnotations;

namespace Consultech.Web.Models.Consultants;

public sealed class ConsultantViewModel
{
    public int Id { get; set; }
    [Display(Name = "Prénom")]
    public string FirstName { get; set; } = string.Empty;
    [Display(Name = "Nom")]
    public string LastName { get; set; } = string.Empty;
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
    [Display(Name = "Date d'embauche")]
    public DateTime StartDate { get; set; }
    [Display(Name = "Disponible")]
    public bool IsAvailable { get; set; } = true;
    [Display(Name = "Compétences")]
    public string Skills { get; set; } = string.Empty; 

    public static ConsultantViewModel FromDTO(DTOs.ConsultantDTO dto) => new()
    {
        Id = dto.Id,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Email = dto.Email,
        StartDate = dto.StartDate,
        IsAvailable = dto.IsAvailable,
        Skills = string.Join(", ", dto.Skills.Select(skill => skill.Title);
    };  
}
