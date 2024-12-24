namespace BusinessLogic.Extensions
{
	public static class IEnumerableTaskExtension
	{
		public static IEnumerable<DataAccess.Entities.Task> FilterByKeyWord(this IEnumerable<DataAccess.Entities.Task> tasks, string? keyWord)
		{
			if (string.IsNullOrEmpty(keyWord))
				return tasks;

			return tasks.Where(t => t.Title.ToLower().Contains(keyWord.ToLower()));
		}

		public static IEnumerable<DataAccess.Entities.Task> FilterByPriority(this IEnumerable<DataAccess.Entities.Task> tasks, string? priority)
		{
			if (string.IsNullOrEmpty(priority))
				return tasks;

			return tasks.Where(t => t.Priority.ToLower() == priority.ToLower());
		}

		public static IEnumerable<DataAccess.Entities.Task> FilterByState(this IEnumerable<DataAccess.Entities.Task> tasks, string? state)
		{
			if (string.IsNullOrEmpty(state))
				return tasks;

			return tasks.Where(t => t.State.ToLower() == state.ToLower());
		}

		public static IEnumerable<DataAccess.Entities.Task> FilterByTimeToFinish(this IEnumerable<DataAccess.Entities.Task> tasks, string? timeToFinish)
		{
			if (string.IsNullOrEmpty(timeToFinish))
				return tasks;

			return tasks.Where(t => t.TimeToFinish == timeToFinish);
		}

		public static IEnumerable<DataAccess.Entities.Task> FilterByEmployee(this IEnumerable<DataAccess.Entities.Task> tasks, int employeeId)
		{
			if (employeeId == 0)
				return tasks;

			return tasks.Where(t => t.Employees.Any(e => e.Id == employeeId));
		}
	}
}
