using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;
using Tests.Integration.Factories;

namespace Tests.Integration.DataAccessTests
{
	public class EmploeeRepositoryTests : IClassFixture<EmployeeFactory>
	{
		private readonly UnitOfWork _unitOfWork;
		public EmploeeRepositoryTests(EmployeeFactory factory) =>
			_unitOfWork = new(factory.Services.GetRequiredService<DataContext>());

		[Theory]
		[InlineData("Oleksandr", "Shevchenko")]
		public async System.Threading.Tasks.Task GetByFullNameAsync_ReturnCorrectEmployee(string name, string surname)
		{
			//Arrange

			//Act
			Employee? dbEmployee = await _unitOfWork.EmployeeRepository.GetByFullNameAsync(name, surname);

			//Assert
			Assert.NotNull(dbEmployee);
			Assert.Equal(name, dbEmployee.User.Name);
			Assert.Equal(surname, dbEmployee.User.Surname);
		}

		[Theory]
		[InlineData(6, "free")]
		[InlineData(7, "loaded")]
		public async System.Threading.Tasks.Task AddAsync_CreateNewEmployee(int userId, string workload)
		{
			//Arrange
			Employee employee = new()
			{
				UserId = userId,
				Workload = workload
			};

			//Act
			await _unitOfWork.EmployeeRepository.AddAsync(employee);
			await _unitOfWork.SaveAsync();
			User? dbUser = await _unitOfWork.UserRepository.GetByIdAsync(employee.UserId);
			Employee? dbEmployee = await _unitOfWork.EmployeeRepository.GetByFullNameAsync(dbUser.Name, dbUser.Surname);


			//Assert
			Assert.NotNull(dbUser);
			Assert.Equal(employee.UserId, dbEmployee.UserId);
			Assert.Equal(employee.Workload, dbEmployee.Workload);
			Assert.Equal(dbUser.Name, dbEmployee.User.Name);
			Assert.Equal(dbUser.Surname, dbEmployee.User.Surname);
		}

		[Theory]
		[InlineData(5, "has few tasks")]
		[InlineData(4, "free")]
		public async System.Threading.Tasks.Task Update_ChangeEmployeeWorkload(int id, string workload)
		{
			//Arrange
			Employee? dbEmployee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
			dbEmployee.Workload = workload;

			//Act
			_unitOfWork.EmployeeRepository.Update(dbEmployee);
			await _unitOfWork.SaveAsync();
			Employee? updatedEmployee = await _unitOfWork.EmployeeRepository.GetByFullNameAsync(dbEmployee.User.Name, dbEmployee.User.Surname);
			User? dbUser = await _unitOfWork.UserRepository.GetByIdAsync(updatedEmployee.UserId);


			//Asert
			Assert.NotNull(updatedEmployee);
			Assert.Equal(workload, updatedEmployee.Workload);
			Assert.Equal(dbUser.Name, updatedEmployee.User.Name);
			Assert.Equal(dbUser.Surname, updatedEmployee.User.Surname);
		}


		[Theory]
		[InlineData(3)]
		public async System.Threading.Tasks.Task DeleteByIdAsync_RemoveEmployee(int id)
		{
			//Arange

			//Act
			await _unitOfWork.UserRepository.DeleteByIdAsync(id);
			await _unitOfWork.SaveAsync();
			Employee? deletedeEmployee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

			//Assert
			Assert.Null(deletedeEmployee);
		}

		[Theory]
		[InlineData("Dmytro", "Kovalchuk")]
		public async System.Threading.Tasks.Task UserRepository_Delete_RemoveEmployee(string name, string surname)
		{
			//Arange
			User? dbUser = await _unitOfWork.UserRepository.GetByFullNameAsync(name, surname);
			Employee? dbEmployee = await _unitOfWork.EmployeeRepository.GetByFullNameAsync(name, surname);

			//Act
			_unitOfWork.UserRepository.Delete(dbUser);
			await _unitOfWork.SaveAsync();
			User? deletedUser = await _unitOfWork.UserRepository.GetByIdAsync(dbUser.Id);
			Employee? deletedEmplyoee = await _unitOfWork.EmployeeRepository.GetByFullNameAsync(name, surname);

			//Assert
			Assert.NotNull(dbEmployee);
			Assert.Null(deletedUser);
			Assert.Null(deletedEmplyoee);
		}
	}
}
