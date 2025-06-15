using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>().HasQueryFilter(t => !t.IsDeleted);

            // CreatedBy relationship
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.CreatedByUser)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // AssignedTo relationship
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Junior ⇒ Senior relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Juniors)
                .WithOne(u => u.Manager)
                .HasForeignKey(u => u.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken) => base.SaveChangesAsync(cancellationToken);

    }
}
