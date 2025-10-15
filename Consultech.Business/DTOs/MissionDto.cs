namespace Consultech.Business.DTOs;

public sealed class MissionDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; } = DateTime.Today;
    
    public DateTime EndDate { get; set; } = DateTime.Today;

    public decimal Budget { get; set; } = 0;

    public ClientDto Client { get; set; } = new();

    public ConsultantDto Consultant { get; set; } = new();
}