using Consultech.Web.DTOs.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Consultech.Web.Models.Skills;

public class SkillInputViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Titre")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Catégorie")]
    public string Category { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Niveau")]
    public SkillLevel Level { get; set; }

    public List<SelectListItem> Levels { get; set; } = new();
}