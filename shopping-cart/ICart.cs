using System;
using System.Collections.Generic;

namespace shopping_cart
{
	public interface ICart
	{
		Guid Id { get; set; }
		Dictionary<Product, int> Items { get; set; }
		decimal Total { get; }

		bool AddItem(Product product, int quantity);
		void CalculateTotal(IPromotionService service);
		bool RemoveItem(Product product);
	}
}