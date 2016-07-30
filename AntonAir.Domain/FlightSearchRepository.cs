using AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories;
using AntonAir.Data.Entities;
using AntonAir.DataAccess.Repository.Interfaces;
using AntonAir.DomainObjects.SearchCriteria;
using AntonAir.DomainObjects.ViewModel;
using System.Linq;

namespace AntonAir.Domain
{
	public class FlightSearchRepository : IReadListModelRepository<FlightViewModel, FlightSearchCriteria>
	{
		private readonly IUnitOfWork unitOfWork;

		public FlightSearchRepository(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public IQueryable<FlightViewModel> All(FlightSearchCriteria criteria)
		{
			return unitOfWork.CreateRepository<Flight>().Query().Select(x => new FlightViewModel());
		}
	}
}