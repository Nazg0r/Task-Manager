namespace BusinessLogic.DTOs
{
	public class FilterDTO
	{
		public string? TitleKeyWord { get; set; }
		public string? Priority { get; set; }
		public string? State { get; set; }
		public string? TimeToFinish { get; set; }
		public int EmployerId { get; set; }
	}
}
