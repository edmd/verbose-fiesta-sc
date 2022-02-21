using shopping_cart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace shopping_cart
{
	public interface IPromotionService
	{
		Task<List<Promotion>> GetPromotions(Dictionary<Product, int> products, string coupon = null);
	}
}