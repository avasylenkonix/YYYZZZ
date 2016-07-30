using AntonAir.CQRS.Infrastructure.Read.Interfaces;
using AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories;
using AntonAir.CQRS.Infrastructure.Read.Interfaces.Services.Single;

namespace AntonAir.CQRS.Infrastructure.Read.Core.Services.Single
{
	public class SingleViewModelService<TModel, TId, TViewModel> : ISingleViewModelService<TModel, TId, TViewModel>
	{
		private readonly IReadRepository<TModel, TId> _repository;
		private readonly IViewModelFactory<TModel, TViewModel> _viewModelFactory;

		public SingleViewModelService(IReadRepository<TModel, TId> repository, IViewModelFactory<TModel, TViewModel> viewModelFactory)
		{
			this._repository = repository;
			this._viewModelFactory = viewModelFactory;
		}

		public TViewModel GetViewModel(TId id)
		{
			var model = this._repository.Get(id);

			var viewModel = this._viewModelFactory.Create(model);

			return viewModel;
		}
	}

	public class SingleViewModelService<TViewModel> : ISingleViewModelService<TViewModel>
	{
		private readonly IViewModelFactory<TViewModel> _viewModelFactory;

		public SingleViewModelService(IViewModelFactory<TViewModel> viewModelFactory)
		{
			this._viewModelFactory = viewModelFactory;
		}

		public TViewModel GetViewModel()
		{
			var viewModel = this._viewModelFactory.Create();

			return viewModel;
		}
	}
}