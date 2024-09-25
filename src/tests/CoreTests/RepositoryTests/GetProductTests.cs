using Core.Repository.Interfaces;
using Core.Repository.Services;
using DB.DB;
using Models.Entities;

namespace CoreTests.RepositoryTests;

public class GetProductTests
{
	[Fact]
	public void GetAllProducts_ReturnsIEnumrableProduct()
	{
		// Arrange
		var ctx = new ApplicationDbContext();
		IProductRepository productRepository = new ProductRepository(ctx);
		List<Product> expected = [
			new Product()
			{
				Name = @"SteelSeries Rival 3 USB Black",
				OrinigLink = "https://some.link.com",
				OriginPrice = 1699,
				CurrentPrice = 1699,
				CreatedAtDate = DateTime.Parse("2024-09-20 01:43:45.434223+03").ToUniversalTime(),
				Id = Guid.Parse("dcee00e5-449e-44bc-9283-262cc3c61245"),
				LastCheckedDate = DateTime.Parse("2024-09-20 01:43:45.434265+03").ToUniversalTime(),
				OriginType = Models.Enums.OriginType.RZ,
				SalePercent = 0,
				UserId = 1
			}
		];


		// Act
		var result = productRepository.GetAllProducts();

		// Assert
		Assert.NotNull(result);
		Assert.Equivalent(expected, result);
	}

	[Theory]
	[InlineData("dcee00e5-449e-44bc-9283-262cc3c61245")]
	public void GetProductById_ReturnsProduct(string id)
	{
		// Arrange
		var ctx = new ApplicationDbContext();
		IProductRepository productRepository = new ProductRepository(ctx);
		var productId = Guid.Parse(id);
		var expected = new Product()
		{
			Name = @"SteelSeries Rival 3 USB Black",
			OrinigLink = "https://some.link.com",
			OriginPrice = 1699,
			CurrentPrice = 1699,
			CreatedAtDate = DateTime.Parse("2024-09-20 01:43:45.434223+03").ToUniversalTime(),
			Id = Guid.Parse("dcee00e5-449e-44bc-9283-262cc3c61245"),
			LastCheckedDate = DateTime.Parse("2024-09-20 01:43:45.434265+03").ToUniversalTime(),
			OriginType = Models.Enums.OriginType.RZ,
			SalePercent = 0,
			UserId = 1
		};

		// Act
		var result = productRepository.GetProductById(productId);

		// Assert
		Assert.NotNull(result);
		Assert.IsType<Product>(result);
		Assert.Equivalent(expected, result);
	}
}
