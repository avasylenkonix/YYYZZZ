using System;

namespace AntonAir.Data.Entities
{
	public class Payment
	{
		public Guid PaymentId { get; set; }

		public virtual Flight Flight { get; set; }

		public virtual Passenger Passenger { get; set; }

		public virtual PaymentType PaymentType { get; set; }

		public string PaymentDetails { get; set; }

		public double Amount { get; set; }

		public DateTime DateTime { get; set; }
	}
}