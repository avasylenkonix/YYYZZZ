using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntonAir.Data.Entities
{
	public class FlightPassenger
	{
		public Guid FlightPassengerId { get; set; }

		public string SeatName { get; set; }

		public string CheckedIn { get; set; }

		public virtual Passenger Passenger { get; set; }

		public virtual Flight Fight { get; set; }
	}
}