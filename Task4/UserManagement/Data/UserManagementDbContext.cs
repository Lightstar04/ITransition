using Microsoft.EntityFrameworkCore;
using UserManagement.Entities;
using UserManagement.Enums;

namespace UserManagement.Data
{
    public class UserManagementDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
            : base(options)
        {
                
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
