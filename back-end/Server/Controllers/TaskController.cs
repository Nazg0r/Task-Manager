using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Services;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
	public class TaskController(ITaskService taskService) : BaseController
	{
		[HttpGet]
		[ProducesResponseType<IEnumerable<TaskModel>>(StatusCodes.Status200OK)]
		[ProducesResponseType<List<Error>>(StatusCodes.Status404NotFound)]
		public async Task<IResult> GetTasks([FromQuery] FilterDTO filter)
		{
			ErrorOr<IEnumerable<TaskModel>> result = await taskService.GetFilteredTasksAsync(filter);

			return result.Match(
				tasks => Results.Ok(tasks),
				errors => Results.NotFound(errors.First())
			);
		}

		[HttpPost]
		[ProducesResponseType<TaskModel>(StatusCodes.Status201Created)]
		[ProducesResponseType<List<Error>>(StatusCodes.Status400BadRequest)]
		public async Task<IResult> CreateTask(TaskDTO taskDTO)
		{
			ErrorOr<TaskModel> result = await taskService.CreateTaskAsync(taskDTO);

			return result.Match(
				task => Results.Ok(task),
				errors => Results.BadRequest(errors.First())
			);
		}

		[HttpPut]
		[ProducesResponseType<TaskModel>(StatusCodes.Status200OK)]
		[ProducesResponseType<List<Error>>(StatusCodes.Status400BadRequest)]
		public async Task<IResult> UpdateTask(TaskUpdateDTO taskDTO)
		{
			ErrorOr<TaskModel> result = await taskService.UpdateTaskAsync(taskDTO);

			return result.Match(
				task => Results.Ok(task),
				errors => Results.BadRequest(errors.First())
			);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType<List<Error>>(StatusCodes.Status404NotFound)]
		public async Task<IResult> DeleteTask(int id)
		{
			ErrorOr<Success> result = await taskService.DeleteTaskAsync(id);

			return result.Match(
				_ => Results.NoContent(),
				errors => Results.NotFound(errors.First())
			);
		}
	}
}
