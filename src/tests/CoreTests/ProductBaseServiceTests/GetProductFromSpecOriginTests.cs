using Core.Services;
using Models.Entities;
using Models.Enums;

namespace CoreTests.ProductBaseServiceTests;

public class GetProductFromSpecOriginTests
{
	[Fact]
	public async Task GetProduct_ReturnMouse()
	{
		// Arrange
		string url = @"https://rozetka.com.ua/ua/steelseries_62513/p179706829/";
		ProductBase expected = new()
		{
			Name = @"Миша SteelSeries Rival 3 USB Black (SS62513)",
			OrinigLink = "https://rozetka.com.ua/ua/steelseries_62513/p179706829/",
			OriginPrice = 1699,
			CurrentPrice = 1699,
			OriginType = OriginType.RZ
		};


		// Act
		var result = await new ProductBaseService().GetProductBaseFromOriginAsync(OriginType.RZ, url);

		// Assert
		Assert.NotNull(result);
		Assert.Equivalent(expected, result);
	}
}
