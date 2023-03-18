using CRUD_Logging.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Logging.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(b =>
            {
                b.ToTable("Employees");
                b.HasKey(k => k.Id);
                b.Property(q => q.Name).HasMaxLength(20).HasColumnName("Name");
                b.Property(l => l.Surname).HasMaxLength(20).HasColumnName("Surname");
                b.Property(l => l.Position).HasMaxLength(20).HasColumnName("Position");
                b.Property(l => l.BirthDate).HasColumnName("BirthDate");
                b.Property(l => l.Salary).HasColumnName("Salary");
                b.Property(l => l.IsManager).HasColumnName("IsManager");
                b.HasIndex(i => i.Name).IsUnique();

                
                //b.Property(x => x.BirthDate).HasDefaultValue(DateTime.Now);
            });
        }
    }
}
