using Core.Services.ShopServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoreTests.ShopServiceTests;

public class GetProductFromLinkTests
{

	[Fact]
	public void GetProductFromRZTKLink_ReturnProductIdAsString0()
	{
		// Arrange 
		string rztkLink = @"https://rozetka.com.ua/ua/steelseries_62513/p179706829/";
		string productId = "179706829";

		// Act
		var result = RZ_Collector.GetIdFromLink(rztkLink);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(productId, result);
	}

	[Fact]
	public void GetProductFromRZTKLink_ReturnProductIdAsString1()
	{
		// Arrange 
		string rztkLink = @"https://rozetka.com.ua/ua/steelseries_62513/p387542565";
		string productId = "387542565";

		// Act
		var result = RZ_Collector.GetIdFromLink(rztkLink);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(productId, result);
	}

	[Fact]
	public void GetPriceWithRegexRZ_ReturnString()
	{
		// Arrange 
		string source = @"{""data"": {""id"": 387542565,""title"": ""Комплект 2 шт. акумуляторні батарейки Smartoools AA 2600 mah + зарядка type-C"",""price"": ""499"",""old_price"": ""559"",""price_pcs"": ""11.97"",""min_month_price"": ""0"",";
		string pattern = @"(\.*)(""price"": ""[1-9]*"")";
		string expected = @"""price"": ""499""";

		// Act
		var result = RZ_Collector.GetWithRegex(source, pattern);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(expected, result);
	}

	[Fact]
	public void GetTitleWithRegexRZ_ReturnString()
	{
		// Arrange 
		string source = @"{""data"": {""id"": 387542565,""title"": ""Комплект 2 шт. акумуляторні батарейки Smartoools AA 2600 mah + зарядка type-C"",""price"": ""499"",""old_price"": ""559"",""price_pcs"": ""11.97"",""min_month_price"": ""0"",";
		string pattern = @"(\.*)(""title"": "".*"",""price"")";
		string expected = @"""title"": ""Комплект 2 шт. акумуляторні батарейки Smartoools AA 2600 mah + зарядка type-C"",""price""";

		// Act
		var result = RZ_Collector.GetWithRegex(source, pattern);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(expected, result);
	}

	[Fact]
	public void GetOldPriceWithRegexRZ_ReturnString()
	{
		// Arrange 
		string source = @"{""data"": {""id"": 387542565,""title"": ""Комплект 2 шт. акумуляторні батарейки Smartoools AA 2600 mah + зарядка type-C"",""price"": ""499"",""old_price"": ""559"",""price_pcs"": ""11.97"",""min_month_price"": ""0"",";
		string pattern = @"(\.*)(""old_price"": ""[1-9]*"")";
		string expected = @"""old_price"": ""559""";

		// Act
		var result = RZ_Collector.GetWithRegex(source, pattern);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(expected, result);
	}
}
