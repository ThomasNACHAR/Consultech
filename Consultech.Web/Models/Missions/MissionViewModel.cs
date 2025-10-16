using System.ComponentModel.DataAnnotations;

namespace Consultech.Web.Models.Missions;

public sealed class MissionViewModel
{
    public int Id { get; set; }
    [Display(Name = "Titre")]
    public string Title { get; set; } = string.Empty;
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;
    [Display(Name = "Date de début")]
    public DateTime StartDate { get; set; }
    [Display(Name = "Date de fin")]
    public DateTime EndDate { get; set; }
    [Display(Name = "Budget")]  
    public decimal Budget { get; set; }
    [Display(Name = "Client")]
    public string ClientCompanyName { get; set; } = string.Empty;
    [Display(Name = "Consultant")]
    public string? ConsultantFullName { get; set; } = null;


    public static MissionViewModel FromDTO(DTOs.MissionDTO mission) => new()
    {
        Id = mission.Id,
        Title = mission.Title,
        Description = mission.Description,
        StartDate = mission.StartDate,
        EndDate = mission.EndDate,
        Budget = mission.Budget,
        ClientCompanyName = mission.Client.CompanyName,
        ConsultantFullName = mission.Consultant is not null ? $"{mission.Consultant.FirstName} {mission.Consultant.LastName}" : null
    };
}
