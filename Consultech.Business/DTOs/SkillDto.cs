using Consultech.DAL.Entities;
using Consultech.DAL.Entities.Enums;

namespace Consultech.Business.DTOs;

public sealed class SkillDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public SkillLevel Level { get; set; } = new();
    
    public List<ConsultantDto> Consultants { get; set; } = new();
}