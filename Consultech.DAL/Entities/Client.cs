using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Consultech.DAL.Entities.Enums;

namespace Consultech.DAL.Entities;

public sealed class Client
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string CompanyName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public ActivitySector ActivitySector { get; set; }
    
    public ICollection<Mission>  Missions { get; set; } = new List<Mission>();
}