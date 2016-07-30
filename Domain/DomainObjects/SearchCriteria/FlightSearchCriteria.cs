using System;

namespace AntonAir.DomainObjects.SearchCriteria
{
	public class FlightSearchCriteria
	{
		public Guid FromCityId { get; set; }

		public Guid ToCityId { get; set; }

		public DateTime? Date { get; set; }

		public int Amount { get; set; }
	}
}