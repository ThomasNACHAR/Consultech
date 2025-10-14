using Consultech.DAL.Entities;
using Consultech.DAL.Entities.Enums;

namespace Consultech.Business.DTOs;

public sealed class ClientDto
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;

    public ActivitySector ActivitySector { get; set; } = new();
    
    public List<MissionDto> Missions { get; set; } = new();
}