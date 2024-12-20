using DataAccess.Interfaces;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
	public class UnitOfWork(DataContext dbContext) : IUnitOfWork
	{
		public IUserRepository UserRepository => new UserRepository(dbContext);

		public IEmployeeRepository EmployeeRepository => new EmployeeRepository(dbContext);

		public ITaskRepository TaskRepository => new TaskRepository(dbContext);

		public Task SaveAsync() => 
			dbContext.SaveChangesAsync();
	}
}
