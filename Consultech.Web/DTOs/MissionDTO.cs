namespace Consultech.Web.DTOs;

public sealed class MissionDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Budget { get; set; }
    public ClientDTO Client { get; set; } = new();
    public ConsultantDTO? Consultant { get; set; }
}
