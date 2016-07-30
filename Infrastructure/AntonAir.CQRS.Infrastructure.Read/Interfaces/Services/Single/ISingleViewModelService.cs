namespace AntonAir.CQRS.Infrastructure.Read.Interfaces.Services.Single
{
	public interface ISingleViewModelService<TModel, TId, TViewModel>
	{
		TViewModel GetViewModel(TId id);
	}

	public interface ISingleViewModelService<TModel, TParam>
	{
		TModel GetViewModel(TParam param);
	}

	public interface ISingleViewModelService<TViewModel>
	{
		TViewModel GetViewModel();
	}
}