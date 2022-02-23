using FluentAssertions;
using Moq;
using shopping_cart.Data;
using shopping_cart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace shopping_cart.Tests
{
	public class CartTests
	{
		protected ICart _sut;
		//protected Mock<IPromotionService> _promotionServiceMock; // Technically this should be fully mocked
		protected Mock<IProductRepository> _productRepositoryMock;
		protected IProductRepository _productRepository;
		protected IPromotionService _promotionService;

		public CartTests()
		{
			_promotionService = new PromotionService();
			_productRepositoryMock = new Mock<IProductRepository>();
			_productRepositoryMock.Setup(x => x.GetProducts()).ReturnsAsync(new List<Product>() {
				new Product { Id = (int)ProductEnum.Bread, Name = ProductEnum.Bread.ToString(), Price = 1.00M },
				new Product { Id = (int)ProductEnum.Butter, Name = ProductEnum.Butter.ToString(), Price = 0.80M },
				new Product { Id = (int)ProductEnum.Milk, Name = ProductEnum.Milk.ToString(), Price = 1.15M } });
			_productRepository = _productRepositoryMock.Object;
			_sut = new Cart(_promotionService);
		}

		[Theory]
		[InlineData(new int[] { (int)ProductEnum.Bread, (int)ProductEnum.Butter, (int)ProductEnum.Milk }, "2.95")]
		[InlineData(new int[] { (int)ProductEnum.Bread, (int)ProductEnum.Bread, (int)ProductEnum.Butter, 
			(int)ProductEnum.Butter }, "3.10")]
		[InlineData(new int[] { (int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk, 
			(int)ProductEnum.Milk }, "3.45")]
		[InlineData(new int[] { (int)ProductEnum.Butter, (int)ProductEnum.Butter, (int)ProductEnum.Bread, 
			(int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk, 
			(int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk, (int)ProductEnum.Milk }, "9.00")]
		public async Task Calculate_cart_total(int [] products, string totalString)
		{
			var total = Convert.ToDecimal(totalString); // You cannot send decimals using Inlinedata
			var productList = await _productRepository.GetProducts();

			foreach(int id in products)
			{
				_sut.AddItem(productList.First(x => x.Id == id), 1);
			}

			// Act
			await _sut.CalculateTotal();

			// Assert
			_sut.Total.Should().Be(total);
		}
	}
}