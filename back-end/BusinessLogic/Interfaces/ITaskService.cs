using BusinessLogic.DTOs;
using BusinessLogic.Models;
using ErrorOr;

namespace BusinessLogic.Interfaces
{
	public interface ITaskService
	{
		Task<ErrorOr<IEnumerable<TaskModel>>> GetFilteredTasksAsync(FilterDTO filter);
		Task<ErrorOr<TaskModel>> CreateTaskAsync(TaskDTO taskDTO);
		Task<ErrorOr<TaskModel>> UpdateTaskAsync(TaskUpdateDTO taskDTO);
		Task<ErrorOr<Success>> DeleteTaskAsync(int id);
	}
}
