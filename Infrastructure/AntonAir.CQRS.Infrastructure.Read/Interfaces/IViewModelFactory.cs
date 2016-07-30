namespace AntonAir.CQRS.Infrastructure.Read.Interfaces
{
	public interface IViewModelFactory<TModel, TViewModel>
	{
		TViewModel Create(TModel model);
	}

	public interface IViewModelFactory<TViewModel>
	{
		TViewModel Create();
	}
}