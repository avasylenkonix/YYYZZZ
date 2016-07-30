using System;

namespace AntonAir.DomainObjects.ViewModel
{
	public class FlightViewModel
	{
		public Guid FlightId { get; set; }

		public CityViewModel FromCity { get; set; }

		public CityViewModel ToCity { get; set; }

		public DateTime Departure { get; set; }

		public double Price { get; set; }
	}
}