using Microsoft.EntityFrameworkCore;

namespace ProblemiAPI.Models
{
    public class ProblemsContext : DbContext
    {
        public ProblemsContext(DbContextOptions<ProblemsContext> options)
            : base(options)
        {
        }

        public DbSet<Problem> Problems { get; set; } = null!;
    }
}
