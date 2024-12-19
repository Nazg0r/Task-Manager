using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	[Table("employees")]
	public class Employee
	{
		[Column("id")] public int Id { get; set; }
		[ForeignKey("user_id")] public int UserId { get; set; }
		[Column("workload")] public string Workload { get; set; }

		public User User { get; set; }
		public IEnumerable<Task> Tasks { get; set; }
	}
}
