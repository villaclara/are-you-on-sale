using Core.Repository.Services;
using CoreTests.Helpers;

namespace CoreTests.RepositoryTests;

public class DeleteProductTests
{
	[Theory]
	[InlineData("22222222-2222-2222-2222-222222222222")]
	public async Task DeleteProduct_ReturnBool(string productId)
	{
		// Arrange 
		var pId = Guid.Parse(productId);
		var ctx = await GetAppContext.GetAppContextDbInMemoryAsync();
		var productRepository = new ProductRepository(ctx);

		// Act
		var result = await productRepository.DeleteProductAsync(pId);

		// Assert
		Assert.True(result);
	}
}
