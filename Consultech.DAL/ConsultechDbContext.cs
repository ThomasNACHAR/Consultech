using Microsoft.EntityFrameworkCore;
using Consultech.DAL.Entities;

namespace Consultech.DAL;

public class ConsultechDbContext : DbContext
{
    public ConsultechDbContext(DbContextOptions<ConsultechDbContext> options)
        : base(options)
    {
    }

    public DbSet<Consultant> Consultants { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Mission> Missions { get; set; }
}