using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
	public class EmployeeController(IEmployeeService employeeService) : BaseController
	{

		[HttpGet]
		[ProducesResponseType<IEnumerable<EmployeeModel>>(StatusCodes.Status200OK)]
		[ProducesResponseType<List<Error>>(StatusCodes.Status404NotFound)]
		public async Task<IResult> GetEmployees()
		{
			ErrorOr<IEnumerable<EmployeeModel>> result = await employeeService.GetEmployeesAsync();

			return result.Match(
				employees => Results.Ok(employees),
				errors => Results.NotFound(errors.First())
			);
		}

		[HttpPost]
		[ProducesResponseType<EmployeeModel>(StatusCodes.Status201Created)]
		[ProducesResponseType<List<Error>>(StatusCodes.Status400BadRequest)]
		public async Task<IResult> CreateNewEmployee(EmployeeDTO employeeDTO)
		{
			ErrorOr<EmployeeModel> result = await employeeService.CreateEmployeeAsync(employeeDTO);

			return result.Match(
				employee => Results.Ok(employee),
				errors => Results.BadRequest(errors.First())
			);
		}

		[HttpPut]
		[ProducesResponseType<EmployeeModel>(StatusCodes.Status200OK)]
		[ProducesResponseType<List<Error>>(StatusCodes.Status400BadRequest)]
		public async Task<IResult> UpdateEmployeeInfo(EmployeeDTO employeeDTO)
		{
			ErrorOr<EmployeeModel> result = await employeeService.UpdateEmployeeAsync(employeeDTO);

			return result.Match(
				employee => Results.Ok(employee),
				errors => Results.BadRequest(errors.First())
			);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType<List<Error>>(StatusCodes.Status404NotFound)]
		public async Task<IResult> DeleteEmployee(int id)
		{
			ErrorOr<Success> result = await employeeService.DeleteEmplyeeAsync(id);

			return result.Match(
				_ => Results.NoContent(),
				errors => Results.BadRequest(errors.First())
			);
		}
	}
}
