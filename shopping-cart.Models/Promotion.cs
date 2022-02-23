using System;

namespace shopping_cart.Models
{
	public class Promotion
	{
		public Guid Id { get; set; }

		public string Description { get; set; }

		public decimal Amount { get; set; }
	}
}
