using Data.Interfaces;
using DataAccess.Entities;

namespace DataAccess.Interfaces
{
	public interface IEmployeeRepository : IRepository<Employee>
	{
		Task<Employee?> GetByFullNameAsync(string name, string surname);
	}
}
