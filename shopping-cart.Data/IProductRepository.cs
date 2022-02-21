using System.Collections.Generic;
using System.Threading.Tasks;

namespace shopping_cart.Data
{
	public interface IProductRepository
	{
		Task<List<Product>> GetProducts();
	}
}