using Core.Interfaces;
using Core.Repository.Interfaces;
using Models.Entities;

namespace Core.Services;

public class TrackProductService(IProductRepository productRepository, IProductBaseService productBaseService) : ITrackProductService
{
	private readonly IProductRepository _productRepository = productRepository;
	private readonly IProductBaseService _productBaseService = productBaseService;

	public event Action<Product>? ProductPriceChanged;

	public async Task DoPriceCheckAllProducts()
	{
		var products = _productRepository.GetAllProducts();
		foreach (var product in products)
		{
			// Get ProductBase from the OriginShop
			var baseProduct = await _productBaseService.GetProductBaseFromOriginAsync(product.OriginType, product.OrinigLink);

			// Compare baseProduct.CurrentPrice with product.CurrentPrice
			if (baseProduct != null)
			{
				if (baseProduct.CurrentPrice < product.CurrentPrice)
				{
					await _productRepository.UpdateProductAsync(new()
					{
						Id = product.Id,
						CreatedAtDate = product.CreatedAtDate,
						CurrentPrice = baseProduct.CurrentPrice,
						LastCheckedDate = DateTime.Now.ToUniversalTime(),
						Name = product.Name,
						OriginPrice = baseProduct.OriginPrice,
						OriginType = product.OriginType,
						OrinigLink = product.OrinigLink,
						UserId = product.UserId,
						SalePercent = (int)(baseProduct.OriginPrice - (baseProduct.CurrentPrice * 100 / baseProduct.OriginPrice))
					});

					// Call the Event if the product.CurrentPrice is lower than was in the DB
					ProductPriceChanged?.Invoke(product);
				}
			}
		}
	}

	public async Task DoPriceCheckForSingleProduct(Product product)
	{
		// Get ProductBase from the OriginShop
		var baseProduct = await _productBaseService.GetProductBaseFromOriginAsync(product.OriginType, product.OrinigLink);

		// Compare baseProduct.CurrentPrice with product.CurrentPrice
		if (baseProduct != null)
		{
			if (baseProduct.CurrentPrice < product.CurrentPrice)
			{
				await _productRepository.UpdateProductAsync(new()
				{
					Id = product.Id,
					CreatedAtDate = product.CreatedAtDate,
					CurrentPrice = baseProduct.CurrentPrice,
					LastCheckedDate = DateTime.Now.ToUniversalTime(),
					Name = product.Name,
					OriginPrice = baseProduct.OriginPrice,
					OriginType = product.OriginType,
					OrinigLink = product.OrinigLink,
					UserId = product.UserId,
					SalePercent = (int)(baseProduct.OriginPrice - (baseProduct.CurrentPrice * 100 / baseProduct.OriginPrice))
				});

				// Call the Event if the product.CurrentPrice is lower than was in the DB
				ProductPriceChanged?.Invoke(product);
			}
		}
	}
}
