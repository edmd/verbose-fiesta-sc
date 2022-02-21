using System;
using System.Collections.Generic;
using System.Linq;

namespace shopping_cart
{
	public class Cart : ICart
	{
		public Guid Id { get; set; }

		public Decimal Total
		{
			get
			{
				return Items.Sum(x => x.Key.Price * x.Value);
			}
		}

		public Dictionary<Product, int> Items { get; set; }
		public Cart() { }

		public bool AddItem(Product product, int quantity)
		{
			// We could add an initial check if there is sufficient stock before adding to the cart
			var item = Items.First(x => x.Key.Id == product.Id);
			if (!item.Equals(default(KeyValuePair<Product, int>)))
			{
				Items.Remove(item.Key);
				Items.Add(product, item.Value + quantity);
				return true;
			}
			else
			{
				Items.Add(product, quantity);
			}


			return false;
		}

		public bool RemoveItem(Product product)
		{
			throw new NotImplementedException();
		}

		// A series of events need to be triggered as the Cart moves through the checkout flow:
		// 1. Fetch delivery options
		// 2. Fetch payment options
		// 3. Calculate Total
		// 4. Process payment
		// 5. Order fulfillment
		// 6. Stock update

		public void CalculateTotal(IPromotionService service) // Move service reference to constructor
		{
			var promotions = service.GetPromotions(Items);
			// Calculate Product totals less promotion totals
		}
	}
}