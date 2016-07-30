namespace AntonAir.Data.Entities
{
	public class PassengerFlight
	{
		public int PassengerFlightId { get; set; }

		public virtual Passenger Passenger { get; set; }

		public virtual Flight Flight { get; set; }
	}
}