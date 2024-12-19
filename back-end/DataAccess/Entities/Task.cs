using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	[Table("users")]
	public class Task
	{
		[Column("id")] public int Id { get; set; }
		[Column("title")] public string Title { get; set; }
		[Column("description")] public string Description { get; set; }
		[Column("priority")] public string Priority { get; set; }
		[Column("state")] public string State { get; set; }
		[Column("time_to_finish")] public DateTime TimeToFinish { get; set; }
		[Column("creation_date")] public DateTime CreationDate { get; set; }
		public IEnumerable<Employee> Employees { get; set; }
	}
}
