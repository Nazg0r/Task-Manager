using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	[Table("employees")]
	public class Employee : BaseEntity
	{
		[ForeignKey("User")][Column("user_id")] public int UserId { get; set; }
		[Column("workload")] public string Workload { get; set; }

		public virtual User User { get; set; }
		public virtual ICollection<Task> Tasks { get; set; }

		public override bool Equals(object? obj)
		{
			return obj is Employee emploee &&
			emploee.Id == Id &&
			emploee.Workload == Workload &&
			emploee.UserId == UserId &&
			emploee.User.Equals(User) &&
			emploee.Tasks.Equals(Tasks);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id);
		}
	}
}
