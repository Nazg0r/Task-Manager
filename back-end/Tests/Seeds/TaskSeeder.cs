using Docker.DotNet.Models;

namespace Tests.Seeds
{
	public static class TaskSeeder
	{
		public static IEnumerable<DataAccess.Entities.Task> PreparedTasks =>
			new DataAccess.Entities.Task[]
			{
				new()
				{
					Title = "Complete project report",
					Description = "Prepare the final report for the project.",
					Priority = "High",
					State = "In Progress",
					TimeToFinish = DateTime.Now.AddHours(4).ToString(),
					CreationDate = DateTime.Now.AddDays(-2).ToString()
				},
				new()
				{
					Title = "Team meeting",
					Description = "Discuss project progress with the team.",
					Priority = "Medium",
					State = "Completed",
					TimeToFinish = DateTime.Now.AddHours(1).ToString(),
					CreationDate = DateTime.Now.AddDays(-1).ToString()
				},
				new()
				{
					Title = "Code review",
					Description = "Review the pull requests from team members.",
					Priority = "Low",
					State = "Pending",
					TimeToFinish = DateTime.Now.AddHours(2).ToString(),
					CreationDate = DateTime.Now.AddDays(-3).ToString()
				},
				new()
				{
					Title = "Prepare presentation",
					Description = "Create slides for the upcoming stakeholder meeting.",
					Priority = "High",
					State = "In Progress",
					TimeToFinish = DateTime.Now.AddHours(6).ToString(),
					CreationDate = DateTime.Now.AddDays(-1).ToString()
				},
				new()
				{
				Title = "User feedback analysis",
				Description = "Analyze feedback received from users in the last quarter.",
				Priority = "Medium",
				State = "Pending",
				TimeToFinish = DateTime.Now.AddHours(3).ToString(),
				CreationDate = DateTime.Now.AddDays(-10).ToString()
			},
			};

		public static string sqlQuery =
			"INSERT INTO \"tasks\" (title, description, priority, state, time_to_finish, creation_date) VALUES" +
			string.Join(", ", PreparedTasks.Select(t => $"('{t.Title}', '{t.Description}', '{t.Priority}', '{t.State}', '{t.TimeToFinish}', '{t.CreationDate}')")) +
			";";
	}
}
