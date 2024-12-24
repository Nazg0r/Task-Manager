using BusinessLogic.Errors;
using BusinessLogic.Interfaces;
using BusinessLogic.Mapper;
using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Interfaces;
using ErrorOr;
using Server;

namespace BusinessLogic.Services
{
	public class EmployeeService(IUnitOfWork unitOfWork) : IEmployeeService
	{
		public async Task<ErrorOr<IEnumerable<EmployeeModel>>> GetEmployeesAsync()
		{
			IEnumerable<Employee> employees = await unitOfWork.EmployeeRepository.GetAllAsync();
			if (!employees.Any())
				return EmployeeErrors.EmployeesNotFound;

			IEnumerable<EmployeeModel> result = employees.Select(e => e.ToModel());

			return result.ToErrorOr();
		}

		public async Task<ErrorOr<EmployeeModel>> CreateEmployeeAsync(EmployeeDTO employeeDTO)
		{
			if (string.IsNullOrEmpty(employeeDTO.Name) || string.IsNullOrEmpty(employeeDTO.Surname))
				return EmployeeErrors.IncorrectEmployeeFullname;

			User? dbUser = await unitOfWork.UserRepository.GetByFullNameAsync(employeeDTO.Name, employeeDTO.Surname);
			if (dbUser is not null)
				return EmployeeErrors.EmployeeAlreadyExists;

			User newUser = new User() 
			{
				Name = employeeDTO.Name,
				Surname = employeeDTO.Surname,
			};

			await unitOfWork.UserRepository.AddAsync(newUser);
			await unitOfWork.SaveAsync();

			User? createdUser = await unitOfWork.UserRepository.GetByFullNameAsync(employeeDTO.Name, employeeDTO.Surname);
			if(createdUser is null)
				return UserErrors.UserNotFound;

			Employee newEmployee = new Employee()
			{
				UserId = newUser.Id,
				Workload = employeeDTO.Workload,
			};

			await unitOfWork.EmployeeRepository.AddAsync(newEmployee);
			await unitOfWork.SaveAsync();

			var createdEmployee = await unitOfWork.EmployeeRepository.GetByFullNameAsync(employeeDTO.Name, employeeDTO.Surname);
			return createdEmployee is null
				? EmployeeErrors.EmployeeNotFound
				: createdEmployee.ToModel().ToErrorOr();
		}

		public async Task<ErrorOr<EmployeeModel>> UpdateEmployeeAsync(EmployeeDTO employeeDTO)
		{
			Employee? employee = await unitOfWork.EmployeeRepository.GetByIdAsync(employeeDTO.Id);
			if (employee is null)
				return EmployeeErrors.EmployeeNotFound;

			UpdateEmployeeFields(employee, employeeDTO);
			await unitOfWork.SaveAsync();

			return employee.ToModel().ToErrorOr();
		}

		public async Task<ErrorOr<Success>> DeleteEmplyeeAsync(int id)
		{
			User? dbUser = await unitOfWork.UserRepository.GetByIdAsync(id);
			if (dbUser is null)
				return UserErrors.UserNotFound;

			await unitOfWork.UserRepository.DeleteByIdAsync(id);
			await unitOfWork.SaveAsync();

			User? deletedUser = await unitOfWork.UserRepository.GetByIdAsync(id);
			if (deletedUser is not null)
				return UserErrors.UserNotRemoved;

			return Result.Success;
		}

		private void UpdateEmployeeFields(Employee employee, EmployeeDTO employeeDTO)
		{
			if (employeeDTO.Name is not null)
				employee.User.Name = employeeDTO.Name;
			if (employeeDTO.Surname is not null)
				employee.User.Surname = employeeDTO.Surname;
			if (employeeDTO.Workload is not null)
				employee.Workload = employeeDTO.Workload;
		}
	}
}
