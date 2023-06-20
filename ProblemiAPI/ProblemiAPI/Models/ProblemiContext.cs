using Microsoft.EntityFrameworkCore;

namespace ProblemiAPI.Models
{
    public class ProblemiContext : DbContext
    {
        public ProblemiContext(DbContextOptions<ProblemiContext> options)
            : base(options)
        {
        }

        public DbSet<Problema> Problemi { get; set; } = null!;
    }
}
