using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Consultech.DAL.Entities;

public sealed class Mission
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required]
    [Precision(18, 2)]
    public decimal Budget { get; set; }

    public int? ClientId { get; set; }
    public Client? Client { get; set; }

    public int? ConsultantId { get; set; }
    public Consultant? Consultant { get; set; }
}