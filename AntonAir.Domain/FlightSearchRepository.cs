using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories;
using AntonAir.Data.Entities;
using AntonAir.DataAccess.Repository.Interfaces;
using AntonAir.Domain.Mapper;
using AntonAir.DomainObjects.SearchCriteria;
using AntonAir.DomainObjects.ViewModel;

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
			var passengerRepo = unitOfWork.CreateRepository<FlightPassenger>().Query();
			var seatsRepo = unitOfWork.CreateRepository<Seat>().Query();

			var result = (from x in unitOfWork.CreateRepository<Flight>().Query()
										let passengersCount = passengerRepo.Count(p => p.Fight.FlightId == x.FlightId)
										let totalSeatCount = seatsRepo.Count()
										where
											x.FromCity.CityId == criteria.FromCityId
											&& x.ToCity.CityId == criteria.ToCityId
											&& DbFunctions.TruncateTime(x.Departure) == DbFunctions.TruncateTime(criteria.Date)
											&& passengersCount < totalSeatCount
										select x)
										.ToList();

			IEnumerable<FlightViewModel> cities =
				result.Select(x => AutoMapperConfig.Mapper.Map<FlightViewModel>(x));

			return cities.AsQueryable();
		}
	}
}