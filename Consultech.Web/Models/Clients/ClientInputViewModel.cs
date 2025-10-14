using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Consultech.Web.Models.Clients;

public sealed class ClientInputViewModel
{
    [HiddenInput]
    public int Id { get; set; }
    [Display(Name = "Nom de l'entreprise")]
    public string CompanyName { get; set; } = string.Empty;
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "L'adresse email n'est pas valide")]
    public string Email { get; set; } = string.Empty;
    [Display(Name = "Adresse")]
    public string Address { get; set; } = string.Empty;
    [Display(Name = "Secteur d'activité")]
    public int ActivitySectorId { get; set; }
    [ValidateNever]
    public List<SelectListItem> ActivitySectors { get; set; } = new();
}
