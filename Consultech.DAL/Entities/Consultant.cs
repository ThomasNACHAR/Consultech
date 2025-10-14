using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultech.DAL.Entities;

public sealed class Consultant
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public DateTime StartDate { get; set; }

    [Required]
    public string Email { get; set; }

    public bool IsAvailable { get; set; } = true;

    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}