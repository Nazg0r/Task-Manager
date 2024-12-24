namespace BusinessLogic.Models
{
	public class TaskModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Priority { get; set; }
		public string State { get; set; }
		public string TimeToFinish { get; set; }
		public string CreationDate { get; set; }
		public IEnumerable<EmployeeModel> Employees { get; set; }
	}
}
