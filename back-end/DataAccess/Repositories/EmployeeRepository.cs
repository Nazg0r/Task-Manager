using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
	public class EmployeeRepository(DataContext dbContext) : Repository<Employee>(dbContext), IEmployeeRepository
	{
		public async Task<Employee?> GetByFullNameAsync(string name, string surname) =>
			await entities.FirstOrDefaultAsync(e => e.User.Name == name && e.User.Surname == surname);
	}
}
