using System.Linq;

namespace AntonAir.DataAccess.Repository.Interfaces
{
	public interface IReadRepository<out TEntity> where TEntity : class
	{
		/// <summary>
		/// Gets a specifC entity.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		TEntity Get(int id);

		/// <summary>
		/// Returns IQuerable object for a specific entity
		/// </summary>
		/// <returns></returns>
		IQueryable<TEntity> Query(bool tracking = true);
	}
}