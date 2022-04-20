using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Strago.Models;

namespace Strago.Data
{
    public class StragoDbContext : DbContext
    {
        public StragoDbContext(DbContextOptions<StragoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; } = null!;
        public DbSet<Experience> Experience { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
    }
}