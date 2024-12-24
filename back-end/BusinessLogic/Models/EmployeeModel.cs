namespace BusinessLogic.Models
{
	public class EmployeeModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Workload { get; set; }
		public IEnumerable<int>? TaskIds { get; set; }
	}
}
