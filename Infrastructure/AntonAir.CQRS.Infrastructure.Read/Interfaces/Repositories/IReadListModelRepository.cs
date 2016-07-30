using System.Linq;

namespace AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories
{
	public interface IReadListModelRepository<TModel, TCriteria>
	{
		IQueryable<TModel> All(TCriteria criteria);
	}
}