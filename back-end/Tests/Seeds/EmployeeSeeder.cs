using DataAccess.Entities;

namespace Tests.Seeds
{
	public static class EmployeeSeeder
	{
		public static IEnumerable<Employee> PreparedEmployees =>
		new Employee[]
		{
				new() { UserId = 1, Workload = "free"},
				new() { UserId = 2, Workload = "free"},
				new() { UserId = 3, Workload = "has few tasks"},
				new() { UserId = 4, Workload = "loaded"},
				new() { UserId = 5, Workload = "has few tasks"},
		};

		public static string sqlQuery =
			"INSERT INTO \"employees\" (user_id, workload) VALUES" +
			string.Join(", ", PreparedEmployees.Select(e => $"('{e.UserId}', '{e.Workload}')")) +
			";";
	}
}
