using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Consultech.DAL.Entities.Enums;

namespace Consultech.DAL.Entities;

public sealed class Skill
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Category { get; set; }
    
    [Required]
    public SkillLevel Level { get; set; }
    
    public ICollection<Consultant> Consultants { get; set; } = new List<Consultant>();
}