using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic.Mapper
{
	public static class EntityToModel
	{
		public static EmployeeModel ToModel(this Employee source)
		{
			IEnumerable<int>? taskIds = null;
			if (source.Tasks != null)
				taskIds = source.Tasks.Select(t => t.Id).ToList();

			return new EmployeeModel
			{
				Id = source.Id,
				Name = source.User.Name,
				Surname = source.User.Surname,
				Workload = source.Workload,
				TaskIds = taskIds
			};
		}

		public static TaskModel ToModel(this DataAccess.Entities.Task source)
		{
			return new TaskModel
			{
				Title = source.Title,
				Description = source.Description,
				Priority = source.Priority,
				State = source.State,
				TimeToFinish = source.TimeToFinish,
				CreationDate = source.CreationDate,
				Employees = source.Employees.Select(e => e.ToModel()).ToList()
			};
		}
	}
}
