using DataAccess.Entities;

namespace Tests.Seeds
{
	public static class UserSeeder
	{
		public static IEnumerable<User> PreparedUsers =>
		new User[]
		{
				new() { Name = "Oleksandr", Surname = "Shevchenko"},
				new() { Name = "Dmytro", Surname = "Kovalchuk"},
				new() { Name = "Andriy", Surname = "Grytsenko"},
				new() { Name = "Volodymyr", Surname = "Tkachenko"},
				new() { Name = "Kateryna", Surname = "Moroz"},
				new() { Name = "Olha", Surname = "Sydenko"},
				new() { Name = "Iryna", Surname = "Petrenko"},
		};

		public static string sqlQuery =
			"INSERT INTO \"users\" (name, surname) VALUES" +
			string.Join(", ", PreparedUsers.Select(p => $"('{p.Name}', '{p.Surname}')")) +
			";";
	}
}
