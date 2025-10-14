namespace Consultech.Web.DTOs;

public sealed class ConsultantDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public bool IsAvailable { get; set; } = true;
    public List<SkillDTO> Skills { get; set; } = new List<SkillDTO>();
}
