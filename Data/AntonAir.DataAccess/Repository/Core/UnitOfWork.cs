using AntonAir.Data.Contexts;
using AntonAir.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Concurrent;

namespace AntonAir.DataAccess.Repository.Core
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ConcurrentDictionary<Type, object> _repoRegistry = new ConcurrentDictionary<Type, object>();
		private readonly AntonAirDbContext _context;

		public UnitOfWork(AntonAirDbContext context)
		{
			this._context = context;
		}

		/// <summary>
		/// Creates the repository.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns></returns>
		public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
		{
			return this._repoRegistry.GetOrAdd(typeof(TEntity), new BaseRepository<TEntity>(this._context)) as IRepository<TEntity>;
		}

		/// <summary>
		/// Saves the changes.
		/// </summary>
		public void SaveChanges()
		{
			this._context.SaveChanges();
		}
	}
}