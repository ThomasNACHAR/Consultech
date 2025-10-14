using Consultech.Web.DTOs.Enums;

namespace Consultech.Web.DTOs;

public sealed class SkillDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public SkillLevel skillLevel { get; set; }
}
