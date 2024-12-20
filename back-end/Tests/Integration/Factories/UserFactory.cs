using DataAccess.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Tests.Seeds;

namespace Tests.Integration.Factories
{
	public class UserFactory : WebApplicationFactory<Program>, IAsyncLifetime
	{
		private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
			.WithUsername("test")
			.WithPassword("password")
			.WithDatabase("user_test_db")
			.Build();

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureTestServices(x =>
			{
				x.Remove(x.Single(a => a.ServiceType == typeof(DbContextOptions<DataContext>)));
				x.AddDbContext<DataContext>(opt =>
				{
					opt.UseNpgsql(_container.GetConnectionString());
				}, ServiceLifetime.Singleton);
			});

		}

		public async Task InitializeAsync()
		{
			await _container.StartAsync();
			using var scope = Services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<DataContext>();
			await context.Database.MigrateAsync();
			await IntegrationTestHelper.AddRecordToDb(UserSeeder.sqlQuery, _container.GetConnectionString());
		}

		public new async Task DisposeAsync()
		{
			await _container.StopAsync();
		}
	}
}
