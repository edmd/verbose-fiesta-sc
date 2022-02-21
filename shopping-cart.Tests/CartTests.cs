using FluentAssertions;
using shopping_cart.Data;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace shopping_cart.Tests
{
	public class CartTests
	{
		[Theory]
		[InlineData(new int[] { (int)ProductEnum.Bread, (int)ProductEnum.Butter, (int)ProductEnum.Milk }, "2.95")]
		[InlineData(new int[] { (int)ProductEnum.Bread, (int)ProductEnum.Bread, (int)ProductEnum.Butter, 
			(int)ProductEnum.Butter }, "3.10")]
		[InlineData(new int[] { (int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk, 
			(int)ProductEnum.Milk }, "3.45")]
		[InlineData(new int[] { (int)ProductEnum.Butter, (int)ProductEnum.Butter, (int)ProductEnum.Bread, 
			(int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk, 
			(int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk }, "2.95")]
		public async Task Calculate_cart_total(int [] products, string totalString)
		{
			var total = Convert.ToDecimal(totalString);

			var productRepository = new ProductRepository();
			var productList = await productRepository.GetProducts();

			var cart = new Cart();

			foreach(int id in products)
			{
				cart.AddItem(productList.First(x => x.Id == id), 1);
			}

			// Act
			cart.CalculateTotal(new PromotionService());

			// Assert
			cart.Total.Should().Be(total);
		}
	}
}