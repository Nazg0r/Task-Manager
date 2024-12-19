namespace DataAccess.Entities
{
	public class Employee
	{
		public int Id { get; set; }
		public string Workload { get; set; }
		public IEnumerable<Task> Tasks { get; set; }
	}
}
