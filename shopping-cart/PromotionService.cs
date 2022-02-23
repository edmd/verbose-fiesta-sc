using shopping_cart.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping_cart
{
	public class PromotionService : IPromotionService
	{
		// We need a Promotion object
		// The PromotionService will return a list of the promotions line items the cart is entitled to
		public async Task<Dictionary<Promotion, int>> GetPromotions(Dictionary<Product, int> products, string coupon = null)
		{
			// The promotion engine should be dynamic
			// The promotion rules should be retrieved from a repo
			// The promotion rules should be order aware
			//	 i.e. rule order should be applied in a prioritised order
			// The promotion engine should be wrapped in a microservice
			// The promotion engine should be genericised RuleAttributes
			var promotions = new Dictionary<Promotion, int>();

			// Buy 2 Butter and get a Bread at 50% off
			var butterPromotions = products.FirstOrDefault(x => x.Key.Id == (int)ProductEnum.Butter).Value / 2;
			var breads = products.FirstOrDefault(x => x.Key.Id == (int)ProductEnum.Bread).Value;
			if (breads > 0 && butterPromotions > 0)
			{
				if (breads >= butterPromotions)
				{
					promotions.Add(
						new Promotion
						{
							Amount = products.First(x => x.Key.Id == (int)ProductEnum.Bread).Key.Price * 0.5M,
							Description = "Half off Bread"
						},
						butterPromotions
					);
				}
				else
				{
					promotions.Add(
						new Promotion
						{
							Amount = products.First(x => x.Key.Id == (int)ProductEnum.Bread).Key.Price * 0.5M,
							Description = "Half off Bread"
						},
						breads
					);
				}
			}

			// Buy 3 Milk and get the 4th milk for free
			var milkPromotions = products.FirstOrDefault(x => x.Key.Id == (int)ProductEnum.Milk).Value / 4;
			if (milkPromotions > 0)
			{
				promotions.Add(
					new Promotion
					{
						Amount = products.First(x => x.Key.Id == (int)ProductEnum.Milk).Key.Price,
						Description = "Buy 3 Get 1 Off Milk"
					},
					milkPromotions
				);
			}

			return promotions;
		}
	}
}