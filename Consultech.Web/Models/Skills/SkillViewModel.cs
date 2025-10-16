using Consultech.Web.DTOs;
using Consultech.Web.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace Consultech.Web.Models.Skills;

public class SkillViewModel
{
    public int Id { get; set; }

    [Display(Name = "Titre")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Catégorie")]
    public string Category { get; set; } = string.Empty;

    [Display(Name = "Niveau")]
    public SkillLevel Level { get; set; }

    [Display(Name = "Consultants associés")]
    public List<string> Consultants { get; set; } = new();

    public static SkillViewModel FromDTO(SkillDTO skill) => new()
    {
        Id = skill.Id,
        Title = skill.Title,
        Category = skill.Category,
        Level = skill.Level,
        Consultants = skill.Consultants.Select(c => c.Email).ToList()
    };
}