namespace AntonAir.DataAccess.Repository.Interfaces
{
	/// <summary>
	///
	/// </summary>
	/// <seealso cref="IRepoAbstractFactory" />
	public interface IUnitOfWork : IRepoAbstractFactory
	{
		/// <summary>
		/// Saves the changes.
		/// </summary>
		void SaveChanges();
	}

	/// <summary>
	///
	/// </summary>
	public interface IRepoAbstractFactory
	{
		/// <summary>
		/// Creates the repository.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns></returns>
		IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;
	}
}