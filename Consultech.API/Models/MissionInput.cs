using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Consultech.API.Models;

public sealed class MissionInput
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate { get; set; } = DateTime.Today;
    [Required]
    [Precision(18, 2)]
    public decimal Budget { get; set; } = 0;
    public int ClientId { get; set; }
    public int? ConsultantId { get; set; }
}
