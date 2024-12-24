using BusinessLogic.Models;
using ErrorOr;
using Server;

namespace BusinessLogic.Interfaces
{
	public interface IEmployeeService
	{
		Task<ErrorOr<IEnumerable<EmployeeModel>>> GetEmployeesAsync();
		Task<ErrorOr<EmployeeModel>> CreateEmployeeAsync(EmployeeDTO employeeDTO);
		Task<ErrorOr<EmployeeModel>> UpdateEmployeeAsync(EmployeeDTO employeeDTO);
		Task<ErrorOr<Success>> DeleteEmplyeeAsync(int id);
	}
}
