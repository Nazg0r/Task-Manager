using Data.Entities;
using Data.Interfaces;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
	public class Repository<TEntity>(DataContext DbContext) : IRepository<TEntity> where TEntity : BaseEntity
	{
		public DbSet<TEntity> entities => DbContext.Set<TEntity>();

		public async Task AddAsync(TEntity entity) =>
			await entities.AddAsync(entity);

		public void Delete(TEntity entity) =>
			 entities.Remove(entity);

		public async Task DeleteByIdAsync(int id)
		{
			TEntity? entity = await entities.FindAsync(id);
			if (entity is not null)
				entities.Remove(entity);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync() =>
			await entities.ToListAsync();

		public async Task<TEntity?> GetByIdAsync(int id) =>
			await entities.FindAsync(id);

		public void Update(TEntity entity) =>
			entities.Update(entity);
	}
}
