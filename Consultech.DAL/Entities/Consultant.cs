using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultech.DAL.Entities;

public sealed class Consultant
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string firstName { get; set; }
    
    [Required]
    public string lastName { get; set; }
    
    public DateTime startDate { get; set; }
    
    [Required]
    public string email { get; set; }
    
    public bool isAvailable { get; set; } = true;
    
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}