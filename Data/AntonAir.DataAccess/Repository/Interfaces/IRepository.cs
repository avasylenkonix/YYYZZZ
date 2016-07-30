namespace AntonAir.DataAccess.Repository.Interfaces
{
	public interface IRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity> where TEntity : class
	{
	}
}