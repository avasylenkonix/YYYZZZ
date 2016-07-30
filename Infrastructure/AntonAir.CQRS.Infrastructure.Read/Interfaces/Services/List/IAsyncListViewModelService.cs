using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntonAir.CQRS.Infrastructure.Read.Interfaces.Services.List
{
	public interface IAsyncListViewModelService<TParam, TViewModel>
	{
		Task<IEnumerable<TViewModel>> GetAsync(TParam param);
	}

	public interface IAsyncListViewModelService<TViewModel>
	{
		Task<IEnumerable<TViewModel>> GetAsync();
	}
}