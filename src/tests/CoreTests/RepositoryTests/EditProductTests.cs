using Core.Repository.Services;
using CoreTests.Helpers;

namespace CoreTests.RepositoryTests;

public class EditProductTests
{
	[Theory]
	[InlineData("11111111-1111-1111-1111-111111111111")]
	public async Task EditProduct_ReturnProductNew(string productId)
	{
		// Arrange 
		var pId = Guid.Parse(productId);
		var ctx = await GetAppContext.GetAppContextDbInMemory();
		var productRepository = new ProductRepository(ctx);
		var product = productRepository.GetProductById(pId);

		// Act
		product.Name = "TestName1_Updated";
		product.CurrentPrice = 10;
		product.SalePercent = 90;
		var result = await productRepository.UpdateProductAsync(product);

		// Assert
		Assert.NotNull(product);
		Assert.Equivalent(product, result);
	}
}
