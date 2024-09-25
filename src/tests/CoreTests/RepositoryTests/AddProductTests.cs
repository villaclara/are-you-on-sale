using Core.Repository.Interfaces;
using Core.Repository.Services;
using CoreTests.Helpers;
using Models.Entities;

namespace CoreTests.RepositoryTests;

public class AddProductTests
{
	[Fact]
	public async Task AddProductToPostGres_ReturnProduct()
	{
		// Arrange
		var ctx = await GetAppContext.GetAppContextDbInMemoryAsync();
		IProductRepository productRepository = new ProductRepository(ctx);
		var product = new Product()
		{
			Name = @"SteelSeries Rival 3 USB Black",
			OrinigLink = "https://some.link.com",
			OriginPrice = 1699,
			CurrentPrice = 1699,
			CreatedAtDate = DateTime.Now.ToUniversalTime(),
			Id = Guid.NewGuid(),
			LastCheckedDate = DateTime.Now.ToUniversalTime(),
			OriginType = Models.Enums.OriginType.RZ,
			SalePercent = 0,
			UserId = 1
		};

		// Act
		var result = await productRepository.CreateProductAsync(product);

		// Assert
		Assert.NotNull(result);
		Assert.IsType<Product>(result);
		Assert.Equivalent(product, result);

	}
}
