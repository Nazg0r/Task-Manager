using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
	public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Entities.Task> Tasks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Entities.Task>()
					.HasMany(t => t.Employees)
					.WithMany(e => e.Tasks)
					.UsingEntity(j => j.ToTable("tasks_employees"));
		}
	}
}
