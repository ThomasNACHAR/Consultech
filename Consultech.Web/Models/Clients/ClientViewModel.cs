using Consultech.Web.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace Consultech.Web.Models.Clients;

public sealed class ClientViewModel
{
    public int Id { get; set; }
    [Display(Name = "Nom de l'entreprise")]
    public string CompanyName { get; set; } = string.Empty;
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "L'adresse email n'est pas valide")]
    public string Email { get; set; } = string.Empty;
    [Display(Name = "Adresse")]
    public string Address { get; set; } = string.Empty;
    [Display(Name = "Secteur d'activité")]
    public string ActivitySector { get; set; } = string.Empty;
    public static ClientViewModel FromDTO(DTOs.ClientDTO client) => new()
    {
        Id = client.Id,
        CompanyName = client.CompanyName,
        Email = client.Email,
        Address = client.Address,
        ActivitySector = client.ActivitySector.ToString()
    };
}
