using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Infrastructure
{
    public class UserManagementDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=pavilion;Initial Catalog=user_management;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users");
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsRequired();
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .Property(p => p.Password)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(s => s.Status)
                .HasDefaultValue(UserStatus.Active);

            base.OnModelCreating(modelBuilder);
        }
    }
}
