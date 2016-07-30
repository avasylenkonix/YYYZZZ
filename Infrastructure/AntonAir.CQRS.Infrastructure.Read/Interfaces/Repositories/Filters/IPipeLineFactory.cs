using System;
using System.Collections.Generic;
using System.Linq;

namespace AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories.Filters
{
	public interface IPipeLineFactory
	{
		IFilterRegistryPipeLine<TModel, TCriteria> Create<TModel, TCriteria>(IEnumerable<IQueryFilter<TModel, TCriteria>> filters, TCriteria criteria);

		IFilterRegistryPipeLine<TModel, TCriteria> Create<TModel, TCriteria>(IEnumerable<Func<IQueryable<TModel>, TCriteria, IQueryable<TModel>>> filters, TCriteria criteria);
	}
}