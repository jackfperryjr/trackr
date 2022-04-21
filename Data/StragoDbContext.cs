using Microsoft.EntityFrameworkCore;
using trackr.Models;
namespace trackr.Data;
public class StragoDbContext : DbContext
{
    public StragoDbContext(DbContextOptions<StragoDbContext> options)
        : base(options)
    {}

    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Experience> Experience { get; set; } = null!;
    public DbSet<Skill> Skills { get; set; } = null!;
}