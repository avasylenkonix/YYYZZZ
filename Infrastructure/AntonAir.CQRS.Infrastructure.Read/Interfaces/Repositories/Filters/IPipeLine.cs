using System;
using System.Linq;

namespace AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories.Filters
{
	public interface IPipeLine<TModel>
	{
		IQueryable<TModel> Execute(IQueryable<TModel> query);
	}

	public interface IFilterRegistryPipeLine<TModel, TCriteria> : IPipeLine<TModel>
	{
		void Register(IQueryFilter<TModel, TCriteria> filter);

		void Register(Func<IQueryable<TModel>, TCriteria, IQueryable<TModel>> lambdaFilter);
	}

	public interface IFilterRegistryPipeLine<TModel> : IPipeLine<TModel>
	{
		void Register(IQueryFilter<TModel> filter);

		void Register(Func<IQueryable<TModel>, IQueryable<TModel>> lambdaFilter);
	}
}