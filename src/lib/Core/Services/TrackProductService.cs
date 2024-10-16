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
		var baseProduct = await _productBaseService.GetProductBaseFromOriginAsync(product.OriginType, product.OrinigLink);

		// Only return here because _productBaseService handles when the product was not retrieved.
		if (baseProduct == null)
		{
			_logger.LogWarning("Product returned from GetProductBaseFromOriginAsync is null ({productName})", product.Name);
			return;
		}

		var whatChanged = WhatProductFieldChanged.None;

		if (baseProduct.Name != product.Name)
		{
			whatChanged = WhatProductFieldChanged.All;
			OnProductChanged(new ProductChangedEventArgs(
				null,
				product.Id,
				product.UserId,
				whatChanged,
				oldValue: product.Name,
				newValue: baseProduct.Name));
		}

		// 1. Compare base prices and do stuff
		if (baseProduct.OriginPrice != product.OriginPrice)
		{
			whatChanged = WhatProductFieldChanged.OriginPrice;

			var newProd = product.NewProductWithUpdatedValues(originPrice: baseProduct.OriginPrice, currentPrice: baseProduct.CurrentPrice);
			await _productRepository.UpdateProductAsync(newProd);
			OnProductChanged(new ProductChangedEventArgs(
				newProd,
				product.Id,
				product.UserId,
				whatChanged,
				product.OriginPrice,
				baseProduct.OriginPrice));
		}

		// 2. Compare Current Prices and do stuff
		if (baseProduct.CurrentPrice != product.CurrentPrice && baseProduct.CurrentPrice != baseProduct.OriginPrice)
		{
			int sale = (int)(baseProduct.OriginPrice - (baseProduct.CurrentPrice * 100 / baseProduct.OriginPrice));
			var newProd = product.NewProductWithUpdatedValues(currentPrice: baseProduct.CurrentPrice, originPrice: baseProduct.OriginPrice, salePercent: sale);
			await _productRepository.UpdateProductAsync(newProd);

			// Call the Event if the product.CurrentPrice is lower than was in the DB
			OnProductChanged(new ProductChangedEventArgs(
				newProd,
				product.Id,
				product.UserId,
				Models.Enums.WhatProductFieldChanged.CurrentPrice,
				baseProduct.CurrentPrice,
				baseProduct.OriginPrice));
		}

		//if (whatChanged != WhatProductFieldChanged.None)
		//{
		//	OnProductChanged(new ProductChangedEventArgs(
		//		product.Id,
		//		product.UserId,
		//		whatChanged,
		//		baseProduct.CurrentPrice,
		//		baseProduct.OriginPrice));
		//}

		_logger.LogInformation("Check END for product ({productName})", product.Name);

	}

	protected virtual void OnProductChanged(ProductChangedEventArgs args)
	{
		_logger.LogInformation("Invoking ProductChanged event for product ({productName})", args.Product.Name);
		ProductChanged?.Invoke(this, args);
	}

}
