using AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntonAir.CQRS.Infrastructure.Read.Core.Repositories.Filters
{
	public class PipeLine<TModel, TCriteria> : IFilterRegistryPipeLine<TModel, TCriteria>
	{
		private readonly IList<IQueryFilter<TModel, TCriteria>> _filters = new List<IQueryFilter<TModel, TCriteria>>();
		private readonly IList<Func<IQueryable<TModel>, TCriteria, IQueryable<TModel>>> _lambdaFilters = new List<Func<IQueryable<TModel>, TCriteria, IQueryable<TModel>>>();
		private TCriteria _criteria;

		public PipeLine(TCriteria criteria)
		{
			this._criteria = criteria;
		}

		public void Register(IQueryFilter<TModel, TCriteria> filter)
		{
			this._filters.Add(filter);
		}

		public void Register(Func<IQueryable<TModel>, TCriteria, IQueryable<TModel>> lambdaFilter)
		{
			this._lambdaFilters.Add(lambdaFilter);
		}

		public IQueryable<TModel> Execute(IQueryable<TModel> query)
		{
			query = this._filters.Aggregate(query, (current, filter) => filter.Filter(current, this._criteria));

			query = this._lambdaFilters.Aggregate(query, (current, filter) => filter.Invoke(current, this._criteria));

			return query;
		}
	}

	public class PipeLine<TModel> : IFilterRegistryPipeLine<TModel>
	{
		private readonly IList<IQueryFilter<TModel>> _filters = new List<IQueryFilter<TModel>>();
		private readonly IList<Func<IQueryable<TModel>, IQueryable<TModel>>> _lambdaFilters = new List<Func<IQueryable<TModel>, IQueryable<TModel>>>();

		public void Register(IQueryFilter<TModel> filter)
		{
			this._filters.Add(filter);
		}

		public void Register(Func<IQueryable<TModel>, IQueryable<TModel>> lambdaFilter)
		{
			this._lambdaFilters.Add(lambdaFilter);
		}

		public IQueryable<TModel> Execute(IQueryable<TModel> query)
		{
			query = this._filters.Aggregate(query, (current, filter) => filter.Filter(current));

			query = this._lambdaFilters.Aggregate(query, (current, filter) => filter.Invoke(current));

			return query;
		}
	}
}