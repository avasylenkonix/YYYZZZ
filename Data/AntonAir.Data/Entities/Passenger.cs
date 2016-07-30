using System;

namespace AntonAir.Data.Entities
{
	public class Passenger
	{
		public Guid PassengerId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Identification { get; set; }
	}
}