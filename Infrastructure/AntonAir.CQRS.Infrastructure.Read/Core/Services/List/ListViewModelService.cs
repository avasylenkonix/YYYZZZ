using AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories;
using AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories.Filters;
using AntonAir.CQRS.Infrastructure.Read.Interfaces.Services.List;
using System.Collections.Generic;

namespace AntonAir.CQRS.Infrastructure.Read.Core.Services.List
{
	public class ListViewModelService<TCriteria, TViewModel> : IListViewModelService<TCriteria, TViewModel>
	{
		private readonly IReadListModelRepository<TViewModel, TCriteria> _repository;
		private readonly IPipeLineFactory _pipeLineFactory;
		private readonly IEnumerable<IQueryFilter<TViewModel, TCriteria>> _filters;

		public ListViewModelService(IReadListModelRepository<TViewModel, TCriteria> repository,
			IEnumerable<IQueryFilter<TViewModel, TCriteria>> filters,
			IPipeLineFactory pipeLineFactory)
		{
			this._repository = repository;
			this._filters = filters;
			this._pipeLineFactory = pipeLineFactory;
		}

		public IEnumerable<TViewModel> Get(TCriteria criteria)
		{
			var dbModels = this._repository.All(criteria);
			var pipleLine = this._pipeLineFactory.Create(this._filters, criteria);
			return pipleLine.Execute(dbModels);
		}
	}
}