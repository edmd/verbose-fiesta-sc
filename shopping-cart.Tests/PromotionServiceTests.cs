using FluentAssertions;
using shopping_cart.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace shopping_cart.Tests
{
	public class PromotionServiceTests
	{
		protected IPromotionService _sut;

		public PromotionServiceTests()
		{
			_sut = new PromotionService();
		}

		[Theory]
		[InlineData(new int[] { (int)ProductEnum.Butter, (int)ProductEnum.Butter }, 0)]
		[InlineData(new int[] { (int)ProductEnum.Butter, (int)ProductEnum.Butter, 
			(int)ProductEnum.Butter, (int)ProductEnum.Butter, (int)ProductEnum.Bread, (int)ProductEnum.Bread }, 2)]
		[InlineData(new int[] { (int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk,
			(int)ProductEnum.Milk }, 1)]
		[InlineData(new int[] { (int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk,
			(int)ProductEnum.Milk, (int)ProductEnum.Butter, (int)ProductEnum.Butter, (int)ProductEnum.Bread }, 2)]
		[InlineData(new int[] { (int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk,
			(int)ProductEnum.Milk, (int)ProductEnum.Butter, (int)ProductEnum.Butter }, 1)]
		public async Task Calculate_total_promotions(int[] products, int totalPromotions)
		{
			// Arrange
			var cartProducts = new Dictionary<Product, int>();
			foreach (var product in products)
			{
				var item = cartProducts.FirstOrDefault(x => x.Key.Id == product);
				if (!item.Equals(default(KeyValuePair<Product, int>)))
				{
					cartProducts.Remove(item.Key);
					cartProducts.Add(new Product { Id = product, Name = ((ProductEnum)product).ToString() }, item.Value + 1);
				}
				else
				{
					cartProducts.Add(new Product { Id = product, Name = ((ProductEnum)product).ToString() }, 1);
				}
			}

			// Act
			var promotions = await _sut.GetPromotions(cartProducts);

			// Assert
			promotions.Sum(x => x.Value).Should().Be(totalPromotions);
		}

		[Theory]
		[InlineData(new int[] { (int)ProductEnum.Butter, (int)ProductEnum.Butter, (int)ProductEnum.Bread }, 1)]
		public async Task Valid_promotion_returned(int[] products, int totalPromotions)
		{
			// Arrange
			var cartProducts = new Dictionary<Product, int>();
			foreach (var product in products)
			{
				var item = cartProducts.FirstOrDefault(x => x.Key.Id == product);
				if (!item.Equals(default(KeyValuePair<Product, int>)))
				{
					cartProducts.Remove(item.Key);
					cartProducts.Add(new Product { Id = product, Name = ((ProductEnum)product).ToString() }, item.Value + 1);
				}
				else
				{
					cartProducts.Add(new Product { Id = product, Name = ((ProductEnum)product).ToString() }, 1);
				}
			}

			// Act
			var promotions = await _sut.GetPromotions(cartProducts);

			// Assert
			promotions.Sum(x => x.Value).Should().Be(totalPromotions);
			promotions.FirstOrDefault().Key.Description.Should().Be("Half off Bread");
		}
	}
}
