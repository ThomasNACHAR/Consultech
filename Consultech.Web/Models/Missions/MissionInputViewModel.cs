using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Consultech.Web.Models.Missions;

public sealed class MissionInputViewModel
{
    public int Id { get; set; }
    [Display(Name = "Titre")]
    public string Title { get; set; } = string.Empty;
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;
    [Display(Name = "Date de début")]
    public DateTime StartDate { get; set; } = DateTime.Today;
    [Display(Name = "Date de fin")]
    public DateTime EndDate { get; set; } = DateTime.Today.AddMonths(1);
    [Display(Name = "Budget")]
    public decimal Budget { get; set; }
    [Display(Name = "Client")]
    public int ClientId { get; set; }
    [ValidateNever]
    public List<SelectListItem> Clients { get; set; } = new();
    [Display(Name = "Consultant (optionnel)")]
    public int? ConsultantId { get; set; }
    [ValidateNever]
    public List<SelectListItem> Consultants { get; set; } = new();
}
