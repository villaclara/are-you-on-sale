using Core.Helpers;
using Models.Entities;

namespace CoreTests.CommonTests;

public class ProductExtensionTests
{
	[Fact]
	public void UpdateProductName_ReturnProduct()
	{
		// Arrange
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
		var newName = "TestName1";

		// Act
		var result = product.NewProductWithUpdatedValues(name: newName);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(newName, result.Name);
	}
}
