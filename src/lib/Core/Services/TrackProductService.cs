using Core.EventArgs;
using Core.Helpers;
using Core.Interfaces;
using Core.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Models.Entities;
using Models.Enums;

namespace Core.Services;

public class TrackProductService(IProductRepository productRepository, IProductBaseService productBaseService, ILogger<TrackProductService> logger) : ITrackProductService
{
	private readonly IProductRepository _productRepository = productRepository;
	private readonly IProductBaseService _productBaseService = productBaseService;
	private readonly ILogger<TrackProductService> _logger = logger;

	public event EventHandler<ProductChangedEventArgs>? ProductChanged;

	public async Task DoPriceCheckAllProductsAsync()
	{
		foreach (var product in _productRepository.GetAllProducts())
		{
			_logger.LogInformation("Check for product ({productName})", product.Name);
			await DoPriceCheckForSingleProductAsync(product);
		}
	}

	public async Task DoPriceCheckForSingleProductAsync(Product product)
	{
		// Get ProductBase from the OriginShop
		var currentBaseProduct = await _productBaseService.GetProductBaseFromOriginAsync(product.OriginType, product.OrinigLink);

		var oldProd = (Product)product.Clone();
		ProductChangedEventArgs args = new(oldProd, null, WhatProductFieldChanged.None);

		// Fail when retrieving the product from Link.
		if (currentBaseProduct == null)
		{
			_logger.LogWarning("Product returned from GetProductBaseFromOriginAsync is null ({productName})", product.Name);
			args.WhatFieldChanged = WhatProductFieldChanged.Error;
		}

		// Base price changed.
		else if (currentBaseProduct.OriginPrice != product.OriginPrice)
		{
			var newProd = product.NewProductWithUpdatedValues(originPrice: currentBaseProduct.OriginPrice, currentPrice: currentBaseProduct.CurrentPrice);

			args.WhatFieldChanged = WhatProductFieldChanged.OriginPrice;
			args.NewProduct = newProd;
			await _productRepository.UpdateProductAsync(newProd);
		}

		// Current price changed.
		else if (currentBaseProduct.CurrentPrice != product.CurrentPrice)
		{
			int sale = (int)(currentBaseProduct.OriginPrice - (currentBaseProduct.CurrentPrice * 100 / currentBaseProduct.OriginPrice));
			var newProd = product.NewProductWithUpdatedValues(currentPrice: currentBaseProduct.CurrentPrice, originPrice: currentBaseProduct.OriginPrice, salePercent: sale);
			args.WhatFieldChanged = WhatProductFieldChanged.CurrentPrice;
			args.NewProduct = newProd;
			await _productRepository.UpdateProductAsync(newProd);
		}

		if (args.WhatFieldChanged != WhatProductFieldChanged.None)
		{
			OnProductChanged(args);
		}


		//var whatChanged = WhatProductFieldChanged.None;

		//if (currentBaseProduct.Name != product.Name)
		//{

		//	whatChanged = WhatProductFieldChanged.All;
		//	var args = new ProductChangedEventArgs(
		//		product,
		//		null,
		//		WhatProductFieldChanged.All);
		//	OnProductChanged(args);
		//}

		//// 1. Compare base prices and do stuff
		//else if (currentBaseProduct.OriginPrice != product.OriginPrice)
		//{
		//	whatChanged = WhatProductFieldChanged.OriginPrice;
		//	var newProd = product.NewProductWithUpdatedValues(originPrice: currentBaseProduct.OriginPrice, currentPrice: currentBaseProduct.CurrentPrice);
		//	if (await _productRepository.UpdateProductAsync(newProd) != null)
		//	{
		//		var args = new ProductChangedEventArgs(product, newProd, WhatProductFieldChanged.OriginPrice);
		//		OnProductChanged(args);
		//	}
		//}

		//// 2. Compare Current Prices and do stuff
		//else if (currentBaseProduct.CurrentPrice != product.CurrentPrice && currentBaseProduct.CurrentPrice != currentBaseProduct.OriginPrice)
		//{
		//	int sale = (int)(currentBaseProduct.OriginPrice - (currentBaseProduct.CurrentPrice * 100 / currentBaseProduct.OriginPrice));
		//	var newProd = product.NewProductWithUpdatedValues(currentPrice: currentBaseProduct.CurrentPrice, originPrice: currentBaseProduct.OriginPrice, salePercent: sale);

		//	whatChanged = WhatProductFieldChanged.CurrentPrice;

		//	var args = new ProductChangedEventArgs(
		//		product,
		//		newProd,
		//		WhatProductFieldChanged.CurrentPrice);

		//	await _productRepository.UpdateProductAsync(newProd);

		//	// Call the Event if the product.CurrentPrice is lower than was in the DB
		//	OnProductChanged(args);
		//}

		//if (whatChanged != WhatProductFieldChanged.None)
		//{
		//	OnProductChanged
		//}




		_logger.LogInformation("Check END for product ({productName})", product.Name);

	}

	protected virtual void OnProductChanged(ProductChangedEventArgs args)
	{
		_logger.LogInformation("Invoking ProductChanged event for product ({productName}) with WhatChanged({WhatChanged})", args.OldProduct.Name, args.WhatFieldChanged);
		ProductChanged?.Invoke(this, args);
	}

}
