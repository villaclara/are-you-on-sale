﻿using Core.EventArgs;
using Core.Repository.Services;
using Core.Services;
using DB.DB;
using Microsoft.Extensions.Logging;
using Models.Entities;

namespace CoreTests.TrackProductServiceTests;

public class TrackProductTest
{
	[Fact]
	public async Task TrackProduct_InvokesEvent()
	{
		// Arrange
		var givenProduct = new Product()
		{
			Name = @"Миша SteelSeries Rival 3 USB Black (SS62513)",
			OrinigLink = "https://rozetka.com.ua/ua/steelseries_62513/p179706829/",
			OriginPrice = 2000,
			CurrentPrice = 2000,
			CreatedAtDate = DateTime.Now.ToUniversalTime(),
			Id = Guid.NewGuid(),
			LastCheckedDate = DateTime.Now.ToUniversalTime(),
			OriginType = Models.Enums.OriginType.RZ,
			SalePercent = 0,
			UserId = 1
		};

		var prodBaseService = new ProductBaseService();
		var ctx = new ApplicationDbContext();
		var prodRepository = new ProductRepository(ctx);
		var logger = new Logger<TrackProductService>(new LoggerFactory());
		var trackService = new TrackProductService(prodRepository, prodBaseService, logger);


		// Act
		bool eventRaised = false;
		trackService.ProductChanged += (object? obj, ProductChangedEventArgs args) => eventRaised = true;
		await trackService.DoPriceCheckForSingleProductAsync(givenProduct);

		// Assert
		Assert.True(eventRaised);

	}
}
