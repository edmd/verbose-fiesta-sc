using System.Collections.Generic;
using System.Threading.Tasks;

namespace shopping_cart.Data
{
	public class ProductRepository : IProductRepository
	{
		public async Task<List<Product>> GetProducts()
		{
			return new List<Product>()
			{
				new Product { Id = (int)ProductEnum.Bread, Name = ProductEnum.Bread.ToString(), Price = 1.00M },
				new Product { Id = (int)ProductEnum.Butter, Name = ProductEnum.Butter.ToString(), Price = 0.80M },
				new Product { Id = (int)ProductEnum.Milk, Name = ProductEnum.Milk.ToString(), Price = 1.15M }
			};
		}
	}
}