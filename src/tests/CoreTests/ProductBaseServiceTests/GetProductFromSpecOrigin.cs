using Core.Services;
using Core.Services.ShopServices;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTests.ProductBaseServiceTests;

public class GetProductFromSpecOrigin
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
			CurrentPrice = 1699
		};


		// Act
		var result = await new ProductBaseService().GetProductBaseFromOriginAsync(OriginType.RZ, url);

		// Assert
		Assert.NotNull(result);
		Assert.Equivalent(expected, result);
	}
}
