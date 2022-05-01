using Microsoft.EntityFrameworkCore;
using trackr.Models;
namespace trackr.Data;
public class trackrDbContext : DbContext
{
    public trackrDbContext(DbContextOptions<trackrDbContext> options)
        : base(options)
    {}

    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Experience> Experience { get; set; } = null!;
    public DbSet<Skill> Skills { get; set; } = null!;
    public DbSet<SkillCategory> SkillCategories { get; set; }
}