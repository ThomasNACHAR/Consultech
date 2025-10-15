using Consultech.Web.DTOs.Enums;

namespace Consultech.Web.DTOs;

public sealed class ClientDTO
{
    public int Id { get; set; }
    
    public string CompanyName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;

    public ActivitySector ActivitySector { get; set; }
}
