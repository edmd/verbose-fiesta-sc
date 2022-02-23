using shopping_cart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping_cart
{
	// The user may or may not be authenticated, i.e. User ref would be optional,
	//		the cart would be cookie persisted until the user is authenticated
	public class Cart : ICart
	{
		private IPromotionService _promotionService;
		public Guid Id { get; set; }
		//public User user { get; set; }
		public Dictionary<Product, int> Items { get; set; }
		public Dictionary<Promotion, int> Promotions { get; set; }

		public Cart(IPromotionService promotionService)
		{
			_promotionService = promotionService;
			Items = new Dictionary<Product, int>();
			Promotions = new Dictionary<Promotion, int>();
		}

		public decimal Total
		{
			get
			{
				this.CalculateTotal().Wait(); // https://stackoverflow.com/a/13735418/384311
				return Items.Sum(x => x.Key.Price * x.Value) - Promotions.Sum(x => x.Key.Amount * x.Value);
			}
		}

		public bool AddItem(Product product, int quantity)
		{
			// We could add an initial check if there is sufficient stock before adding to the cart
			//		return false;
			var item = Items.FirstOrDefault(x => x.Key.Id == product.Id);
			if (!item.Equals(default(KeyValuePair<Product, int>)))
			{
				Items.Remove(item.Key);
				Items.Add(product, item.Value + quantity);
			}
			else
			{
				Items.Add(product, quantity);
			}

			return true;
		}

		public bool RemoveItem(Product product)
		{
			throw new NotImplementedException();
		}

		// A series of events need to be triggered as the Cart moves through the checkout flow:
		// - Fetch delivery options
		// - Fetch payment options
		// - Calculate Total
		// - Process payment
		// - Order creation, messages sent on bus topics (e.g. emails)
		// - Stock levels update
		public async Task CalculateTotal()
		{
			Promotions = await _promotionService.GetPromotions(Items);
		}
	}
}