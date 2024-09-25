using Core.Helpers;
using Core.Interfaces;
using Core.Repository.Interfaces;
using Models.Entities;

namespace Core.Services;

public class TrackProductService(IProductRepository productRepository, IProductBaseService productBaseService) : ITrackProductService
{
	private readonly IProductRepository _productRepository = productRepository;
	private readonly IProductBaseService _productBaseService = productBaseService;

	public event Action<Product>? ProductPriceChanged;

	public async Task DoPriceCheckAllProductsAsync()
	{
		foreach (var product in _productRepository.GetAllProducts())
		{
			await DoPriceCheckForSingleProductAsync(product);
		}
	}

	public async Task DoPriceCheckForSingleProductAsync(Product product)
	{
		// Get ProductBase from the OriginShop
		var baseProduct = await _productBaseService.GetProductBaseFromOriginAsync(product.OriginType, product.OrinigLink);

		// Compare baseProduct.CurrentPrice with product.CurrentPrice
		if (baseProduct != null)
		{
			if (baseProduct.CurrentPrice < product.CurrentPrice)
			{
				int sale = (int)(baseProduct.OriginPrice - (baseProduct.CurrentPrice * 100 / baseProduct.OriginPrice));
				var newProd = product.NewProductWithUpdatedValues(currentPrice: baseProduct.CurrentPrice, originPrice: baseProduct.OriginPrice, salePercent: sale);
				await _productRepository.UpdateProductAsync(newProd);

				// Call the Event if the product.CurrentPrice is lower than was in the DB
				OnProductPriceChanged(product);
			}
		}
	}

	protected virtual void OnProductPriceChanged(Product product)
	{
		ProductPriceChanged?.Invoke(product);
	}

}
