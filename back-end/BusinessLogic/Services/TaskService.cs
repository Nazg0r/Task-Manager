using BusinessLogic.DTOs;
using BusinessLogic.Errors;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using BusinessLogic.Mapper;
using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Interfaces;
using ErrorOr;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
	public class TaskService(IUnitOfWork unitOfWork) : ITaskService
	{
		public async Task<ErrorOr<IEnumerable<TaskModel>>> GetFilteredTasksAsync(FilterDTO filter)
		{
			IEnumerable<DataAccess.Entities.Task> tasks = await unitOfWork.TaskRepository.GetAllAsync();
			if (!tasks.Any())
				return TaskErrors.TasksNotFound;

			IEnumerable<DataAccess.Entities.Task> filteredTasks = tasks
				.FilterByKeyWord(filter.TitleKeyWord)
				.FilterByPriority(filter.Priority)
				.FilterByState(filter.State)
				.FilterByTimeToFinish(filter.TimeToFinish)
				.FilterByEmployee(filter.EmployerId)
				.OrderByDescending(t => t.CreationDate);

			IEnumerable<TaskModel> result = filteredTasks.Select(t => t.ToModel());

			return result.ToErrorOr();
		}

		public async Task<ErrorOr<TaskModel>> CreateTaskAsync(TaskDTO taskDTO)
		{
			DataAccess.Entities.Task newTask = new()
			{
				Title = taskDTO.Title,
				Description = taskDTO.Description,
				Priority = taskDTO.Priority,
				State = taskDTO.State,
				TimeToFinish = taskDTO.TimeToFinish,
				CreationDate = DateTime.Now.ToString()
			};

			var result = await SetNewContractors(newTask, taskDTO.EmployeeIds!);

			await unitOfWork.TaskRepository.AddAsync(newTask);
			await unitOfWork.SaveAsync();

			return result.Match(
				_ => newTask.ToModel().ToErrorOr(),
				error => error
			);
		}

		public async Task<ErrorOr<TaskModel>> UpdateTaskAsync(TaskUpdateDTO taskDTO)
		{
			DataAccess.Entities.Task? task = await unitOfWork.TaskRepository.GetByIdAsync(taskDTO.Id);
			if (task is null)
				return TaskErrors.TaskNotFound;

			UpdateTaskFields(task, taskDTO);
			var result = await SetNewContractors(task, taskDTO.EmployeeIds!);

			await unitOfWork.SaveAsync();

			return result.Match(
				_ => task.ToModel().ToErrorOr(),
				error => error
			);
		}

		public async Task<ErrorOr<Success>> DeleteTaskAsync(int id)
		{
			DataAccess.Entities.Task? task = await unitOfWork.TaskRepository.GetByIdAsync(id);
			if (task is null)
				return TaskErrors.TaskNotFound;

			await unitOfWork.TaskRepository.DeleteByIdAsync(task.Id);
			await unitOfWork.SaveAsync();

			return Result.Success;
		}



		private void UpdateTaskFields(DataAccess.Entities.Task task, TaskUpdateDTO taskDTO)
		{
			if (!string.IsNullOrEmpty(taskDTO.Title))
				task.Title = taskDTO.Title;
			if (!string.IsNullOrEmpty(taskDTO.Description))
				task.Description = taskDTO.Description;
			if (!string.IsNullOrEmpty(taskDTO.Priority))
				task.Priority = taskDTO.Priority;
			if (!string.IsNullOrEmpty(taskDTO.State))
				task.State = taskDTO.State;
			if (!string.IsNullOrEmpty(taskDTO.TimeToFinish))
				task.TimeToFinish = taskDTO.TimeToFinish;
		}

		private async Task<ErrorOr<Success>> SetNewContractors(DataAccess.Entities.Task task, IEnumerable<int> employeeIds)
		{
			if (employeeIds is not null && employeeIds.Any())
			{
				task.Employees = new List<Employee>();

				IEnumerable<Employee> contractors = await unitOfWork.EmployeeRepository.GetAllAsync();
				contractors = contractors.Where(e => employeeIds.Contains(e.Id));

				if (!contractors.Any() || contractors.Count() != employeeIds.Count())
					return EmployeeErrors.EmployeesNotFound;

				foreach (var contractor in contractors)
				{
					task.Employees.Add(contractor);
					contractor.Tasks.Add(task);
				}
			}
			else
			{
				foreach (var employee in task.Employees)
				{
					employee.Tasks.Remove(task);
					task.Employees.Remove(employee);
				}
			}

			return Result.Success;
		}
	}
}
