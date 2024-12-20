using Npgsql;

namespace Tests.Integration
{
	public static class IntegrationTestHelper
	{
		public async static Task<NpgsqlConnection> ConnectToDb(string connectionStr)
		{
			var connection = new NpgsqlConnection(connectionStr);
			await connection.OpenAsync();

			return connection;
		}

		public static async Task AddRecordToDb(string query, string connectionStr)
		{
			using var connection = await ConnectToDb(connectionStr);

			using var command = new NpgsqlCommand(query, connection);
			command.CommandType = System.Data.CommandType.Text;
			await command.ExecuteNonQueryAsync();

			command.Dispose();
			await connection.CloseAsync();
		}
	}
}
