using System.Collections.Generic;
using System.Linq;

namespace AntonAir.CQRS.Infrastructure.Read.Core.Repositories.Filters
{
	public abstract class QueryFilterBase
	{
		protected bool HasCriteria<T>(IEnumerable<T> criteria)
		{
			return criteria != null && criteria.Any();
		}
	}
}