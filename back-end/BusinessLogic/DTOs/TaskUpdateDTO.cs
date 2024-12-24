namespace BusinessLogic.DTOs
{
	public class TaskUpdateDTO
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Priority { get; set; }
		public string? State { get; set; }
		public string? TimeToFinish { get; set; }
		public IEnumerable<int>? EmployeeIds { get; set; }
	}
}
