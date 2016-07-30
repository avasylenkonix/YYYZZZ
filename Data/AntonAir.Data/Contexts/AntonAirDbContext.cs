using AntonAir.Data.Entities;
using System.Data.Entity;

namespace AntonAir.Data.Contexts
{
	public class AntonAirDbContext : DbContext
	{
		public AntonAirDbContext() : base("AntonAirDBContext")
		{
		}

		public DbSet<City> Cities { get; set; }

		public DbSet<Flight> Flights { get; set; }

		public DbSet<FlightPassenger> FlightPassengers { get; set; }

		public DbSet<Passenger> Passengers { get; set; }

		public DbSet<PassengerFlight> PassengerFlights { get; set; }

		public DbSet<Payment> Payments { get; set; }

		public DbSet<PaymentType> PaymentTypes { get; set; }

		public DbSet<Seat> Seats { get; set; }
	}
}