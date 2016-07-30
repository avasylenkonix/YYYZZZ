using System.Collections.Generic;

namespace AntonAir.DataAccess.Repository.Interfaces
{
	public interface IWriteRepository<in TEntity> where TEntity : class
	{
		/// <summary>
		/// Adds the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Add(TEntity entity);

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Delete(TEntity entity);

		/// <summary>
		/// Adds the or update.
		/// This method must be used carefuly since it is making calls to the data base for every item we are adding.
		/// Use only if really necesery.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void AddOrUpdate(TEntity entity);/// <summary>

		/// Adds range of entities.
		/// </summary>
		/// <param name="entity">The entities collection.</param>
		void AddRange(IEnumerable<TEntity> entities);

		/// <summary>
		/// Removes range of entities.
		/// </summary>
		/// <param name="entity">The entities collection.</param>
		void RemoveRange(IEnumerable<TEntity> entities);
	}
}