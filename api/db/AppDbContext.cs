using domain;
using Microsoft.EntityFrameworkCore;

namespace api.db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        { 
            this.Database.EnsureCreated();
        }

        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FinancialTransaction>().ToTable("FinancialTransaction");

            base.OnModelCreating(builder);
        }
    }
}