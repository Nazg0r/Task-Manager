namespace DataAccess.Entities
{
	public class Task
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Priority { get; set; }
		public string State { get; set; }
		public DateTime TimeToFinish { get; set; }
		public DateTime CreationDate { get; set; }
		public IEnumerable<Employee> Employees { get; set; }
	}
}
