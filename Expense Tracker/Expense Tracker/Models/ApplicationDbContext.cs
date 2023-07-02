using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options) { }
       

        public DbSet<Trasaction> Trasactions { get; set; }
        public DbSet<Category> Categories { get; set; }
       
    }
}
