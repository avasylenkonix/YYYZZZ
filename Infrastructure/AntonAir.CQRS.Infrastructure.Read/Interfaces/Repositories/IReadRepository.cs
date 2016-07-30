namespace AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories
{
	public interface IReadRepository<TModel, TCriteria>
	{
		TModel Get(TCriteria param);
	}

	public interface IReadRepository<TModel>
	{
		TModel Get();
	}
}