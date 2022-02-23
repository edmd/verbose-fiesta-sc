using FluentAssertions;
using shopping_cart.Data;
using System.Threading.Tasks;
using Xunit;

namespace shopping_cart.Tests
{
	public class ProductRepositoryTests
	{
		protected IProductRepository _sut;

		public ProductRepositoryTests()
		{
			_sut = new ProductRepository();
		}

		[Fact]
		public async Task ProductRepository_GetProducts()
		{
			// Act
			var products = await _sut.GetProducts();

			// Assert
			products.Should().NotBeEmpty();
			products.Count.Should().Be(3);
			Assert.Contains(products, x => x.Id == (int)ProductEnum.Bread);
			Assert.Contains(products, x => x.Id == (int)ProductEnum.Butter);
			Assert.Contains(products, x => x.Id == (int)ProductEnum.Milk);
		}

		// test for bad requests
		// test for repo timeout
	}
}
