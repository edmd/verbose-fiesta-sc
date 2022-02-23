using shopping_cart.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace shopping_cart
{
	public interface ICart
	{
		Guid Id { get; set; }
		Dictionary<Product, int> Items { get; set; }
		Dictionary<Promotion, int> Promotions { get; set; }
		decimal Total { get; }
		bool AddItem(Product product, int quantity);
		Task CalculateTotal();
		bool RemoveItem(Product product);
	}
}