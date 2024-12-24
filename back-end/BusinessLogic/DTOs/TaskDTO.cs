namespace BusinessLogic.DTOs
{
	public class TaskDTO
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Priority { get; set; } = "Low";
		public string State { get; set; } = "Pending";
		public string TimeToFinish { get; set; }
		public IEnumerable<int> EmployeeIds { get; set; }
	}
}
