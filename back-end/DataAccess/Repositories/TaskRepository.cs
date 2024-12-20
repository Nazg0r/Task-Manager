using DataAccess.Data;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
	public class TaskRepository(DataContext dbVontext) : Repository<Entities.Task>(dbVontext), ITaskRepository
	{
	}
}
