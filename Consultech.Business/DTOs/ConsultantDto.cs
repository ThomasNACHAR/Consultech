namespace Consultech.Business.DTOs;

public sealed class ConsultantDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; } = DateTime.Today;

    public string Email { get; set; } = string.Empty;
    
    public bool IsAvailable { get; set; } = true;
    
    public List<SkillDto> Skills { get; set; } = new();
}