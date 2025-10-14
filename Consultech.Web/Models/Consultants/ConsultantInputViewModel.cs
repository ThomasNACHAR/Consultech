using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Consultech.Web.Models.Consultants;

public sealed class ConsultantInputViewModel
{
    [HiddenInput]
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
    [Display(Name = "Compétences (séparées par des virgules)")]
    public List<int> SkillsId { get; set; } = new();
    [ValidateNever]
    public List<SelectListItem> Skills { get; set; } = new();
}
