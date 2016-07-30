using System;

namespace AntonAir.Data.Entities
{
	public class Flight
	{
		public Guid FlightId { get; set; }

		public virtual City FromCity { get; set; }

		public virtual City ToCity { get; set; }

		public DateTime Departure { get; set; }

		public double Price { get; set; }
	}
}