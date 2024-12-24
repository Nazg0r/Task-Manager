namespace Server
{
	public class EmployeeDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string Workload { get; set; } = "free";
	}
}
