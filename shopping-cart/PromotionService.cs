using shopping_cart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace shopping_cart
{
	public class PromotionService : IPromotionService
	{
		// We need a Promotion object
		// The PromotionService will return a list of the promotions line items the cart is entitled to
		public async Task<List<Promotion>> GetPromotions(Dictionary<Product, int> products, string coupon = null)
		{
			return null;
		}
	}
}