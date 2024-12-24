using ErrorOr;

namespace BusinessLogic.Errors
{
	public static class EmployeeErrors
	{
		public static Error EmployeesNotFound =>
			Error.Custom(ErrorTypes.NotFound, "Employees.NotFound", "Failed to get all employees.");

		public static Error EmployeeNotFound =>
			Error.Custom(ErrorTypes.NotFound, "Employee.NotFound", $"Failed to get employee.");
		public static Error EmployeeAlreadyExists =>
			Error.Custom(ErrorTypes.AlreadyExists, "Employee.AlreadyExists", "Employee with such first name and last name already exist.");

		public static Error IncorrectEmployeeFullname =>
			Error.Custom(ErrorTypes.WrongInfo, "Employee.IncorrectFullname", $"Name or surname has am incorrect value.");

	}
}
