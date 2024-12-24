using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Server.Extensions
{
	public static class AppServiceExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration conf)
		{
			services.AddControllers();

			services.AddDbContext<DataContext>(opt =>
			{
				opt.UseLazyLoadingProxies()
				.UseNpgsql(conf.GetConnectionString("DbConnection"));
			}, ServiceLifetime.Singleton);

			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
			services.AddSingleton<ITaskRepository, TaskRepository>();
			services.AddSingleton<IUnitOfWork, UnitOfWork>();

			services.AddSingleton<IEmployeeService, EmployeeService>();
			return services;
		}
	}
}
