using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;
using Tests.Seeds;

namespace Tests.Integration.DataAccessTests
{
	public class TaskRepositoryTests : IClassFixture<Factories.TaskFactory>
	{
		private readonly UnitOfWork _unitOfWork;
		public TaskRepositoryTests(Factories.TaskFactory factory) =>
			_unitOfWork = new(factory.Services.GetRequiredService<DataContext>());

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public async System.Threading.Tasks.Task AddAsync_CreateNewTask(int caseId)
		{
			//Arrange
			DataAccess.Entities.Task newTask = PreparedTasks[caseId];

			//Act
			await _unitOfWork.TaskRepository.AddAsync(newTask);
			await _unitOfWork.SaveAsync();
			DataAccess.Entities.Task? dbTask = await _unitOfWork.TaskRepository.GetByIdAsync(newTask.Id);

			//Assert
			Assert.NotNull(dbTask);
			Assert.Equal(newTask, dbTask);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public async System.Threading.Tasks.Task Update_ChangeTaskFields(int caseId)
		{
			//Arrange
			DataAccess.Entities.Task updatedTask = UpdatedTasks[caseId];


			//Act
			_unitOfWork.TaskRepository.Update(updatedTask);
			await _unitOfWork.SaveAsync();
			DataAccess.Entities.Task? dbUpdatedTask = await _unitOfWork.TaskRepository.GetByIdAsync(updatedTask.Id);

			//Asert
			Assert.NotNull(dbUpdatedTask);
			Assert.Equal(updatedTask, dbUpdatedTask);
		}

		[Theory]
		[InlineData(3)]
		[InlineData(4)]
		public async System.Threading.Tasks.Task Delete_ChangeTaskFields(int caseId)
		{
			//Arrange
			DataAccess.Entities.Task? dbTask = await _unitOfWork.TaskRepository.GetByIdAsync(caseId);

			//Act
			await _unitOfWork.TaskRepository.DeleteByIdAsync(caseId);
			await _unitOfWork.SaveAsync();
			DataAccess.Entities.Task? deletedTask = await _unitOfWork.TaskRepository.GetByIdAsync(caseId);

			//Asert
			Assert.Null(deletedTask);
			Assert.NotNull(dbTask);
		}

		[Theory]
		[InlineData(5, 2)]
		public async System.Threading.Tasks.Task AddEmployee_ChangeTaskEmployees(int caseId, int employeeId)
		{
			//Arrange
			DataAccess.Entities.Task? dbTask = await _unitOfWork.TaskRepository.GetByIdAsync(caseId);
			Employee employee = EmployeeSeeder.PreparedEmployees.FirstOrDefault(e => e.UserId == employeeId)!;

			//Act
			dbTask.Employees.Add(employee);
			await _unitOfWork.SaveAsync();
			DataAccess.Entities.Task? updatedTask = await _unitOfWork.TaskRepository.GetByIdAsync(caseId);

			//Asert
			Assert.NotNull(updatedTask);
			Assert.Equal(employee, updatedTask.Employees.FirstOrDefault(e => e.UserId == employeeId));
		}


		public static List<DataAccess.Entities.Task> PreparedTasks =>
			new List<DataAccess.Entities.Task>
			{
				new()
				{
					Id = 101,
					Title = "Update documentation",
					Description = "Add the latest changes to the API documentation.",
					Priority = "Low",
					State = "Completed",
					TimeToFinish = DateTime.Now.AddHours(-5).ToString(),
					CreationDate = DateTime.Now.AddDays(-5).ToString()
				},
				new()
				{
					Id = 102,
					Title = "Database optimization",
					Description = "Optimize queries for better performance.",
					Priority = "High",
					State = "In Progress",
					TimeToFinish = DateTime.Now.AddDays(1).ToString(),
					CreationDate = DateTime.Now.AddDays(-7).ToString()
				},
			};

		public static List<DataAccess.Entities.Task> UpdatedTasks =>
			new List<DataAccess.Entities.Task>
			{
				new()
				{
					Id = 1,
					Title = "Complete project report very fast",
					Description = "Prepare the final report for the project.",
					Priority = "High",
					State = "In Progress",
					TimeToFinish = DateTime.Now.ToString(),
					CreationDate = DateTime.Now.AddDays(-2).ToString()
				},
				new()
				{
					Id = 2,
					Title = "Team meeting",
					Description = "Discuss project progress with the boss and team.",
					Priority = "High",
					State = "In Progress",
					TimeToFinish = DateTime.Now.AddHours(-4).ToString(),
					CreationDate = DateTime.Now.AddDays(-1).ToString()
				},
			};
	}
}
