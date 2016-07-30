using AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntonAir.CQRS.Infrastructure.Read.Core.Repositories.Filters
{
	public class PipeLineFactory : IPipeLineFactory
	{
		public IFilterRegistryPipeLine<TModel, TCriteria> Create<TModel, TCriteria>(IEnumerable<IQueryFilter<TModel, TCriteria>> filters, TCriteria criteria)
		{
			var pipeLine = new PipeLine<TModel, TCriteria>(criteria);

			if (filters != null)
			{
				foreach (var filter in filters)
				{
					pipeLine.Register(filter);
				}
			}
			return pipeLine;
		}

		public IFilterRegistryPipeLine<TModel, TCriteria> Create<TModel, TCriteria>(IEnumerable<Func<IQueryable<TModel>, TCriteria, IQueryable<TModel>>> filters, TCriteria criteria)
		{
			var pipeLine = new PipeLine<TModel, TCriteria>(criteria);
			foreach (var filter in filters)
			{
				pipeLine.Register(filter);
			}
			return pipeLine;
		}
	}
}