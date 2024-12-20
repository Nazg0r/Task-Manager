using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
	public class UserRepository(DataContext dbContext) : Repository<User>(dbContext), IUserRepository
	{
		public async Task<User?> GetByFullNameAsync(string name, string surname) =>
			await entities.FirstOrDefaultAsync(u => u.Name == name && u.Surname == surname);
	}
}
