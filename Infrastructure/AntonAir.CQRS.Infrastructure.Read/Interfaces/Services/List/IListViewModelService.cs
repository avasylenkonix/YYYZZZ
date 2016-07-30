using System.Collections.Generic;

namespace AntonAir.CQRS.Infrastructure.Read.Interfaces.Services.List
{
	public interface IListViewModelService<TModel, TCriteria, TViewModel>
	{
		IEnumerable<TModel> GetModels(TCriteria criteria);

		IViewModelFactory<TModel, TViewModel> GetViewModelFactory();
	}

	public interface IListViewModelService<TParam, TViewModel>
	{
		IEnumerable<TViewModel> Get(TParam param);
	}

	public interface IListViewModelService<TViewModel>
	{
		IEnumerable<TViewModel> Get();
	}
}