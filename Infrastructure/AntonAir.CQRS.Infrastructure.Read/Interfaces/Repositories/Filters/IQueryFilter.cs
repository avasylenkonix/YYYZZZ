using System.Linq;

namespace AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories.Filters
{
	public interface IQueryFilter<TModel, TCriteria>
	{
		IQueryable<TModel> Filter(IQueryable<TModel> query, TCriteria criteria);
	}

	public interface IQueryFilter<TModel>
	{
		IQueryable<TModel> Filter(IQueryable<TModel> query);
	}
}