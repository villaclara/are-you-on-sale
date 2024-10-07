using Core.Interfaces;
using Core.Repository.Interfaces;
using Models.Entities;
using Serilog;

namespace Core.Services;

public class ProductService(ITrackProductService trackProductService,
	IProductRepository productRepository) : IProductService
{
	public ITrackProductService TrackProductService { get; } = trackProductService;

	private readonly IProductRepository _productRepository = productRepository;

	public async Task AddProductToUserAsync(long userId, ProductBase product)
	{
		Log.Information("{@Method} - add product (@product) to userid(@userid)", nameof(AddProductToUserAsync), product, userId);
		var fullProduct = new Product()
		{
			Id = Guid.NewGuid(),
			Name = product.Name,
			OriginPrice = product.OriginPrice,
			OriginType = product.OriginType,
			CurrentPrice = product.OriginPrice,
			OrinigLink = product.OrinigLink,
			CreatedAtDate = DateTime.Now.ToUniversalTime(),
			LastCheckedDate = DateTime.Now.ToUniversalTime(),
			SalePercent = 0,
			UserId = userId
		};

		await _productRepository.CreateProductAsync(fullProduct);
	}

	public async Task DeleteProductOfUserAsync(long userId, Guid productId)
	{
		var prod = _productRepository.GetProductById(productId);
		if (prod == null || prod.UserId != userId)
		{
			Log.Error("{@Method} - Product not found/Product does not belong to user.", nameof(DeleteProductOfUserAsync));
			throw new ArgumentException("Product not found/Product does not belong to user.");
		}

		await _productRepository.DeleteProductAsync(productId);
	}

	public IEnumerable<Product> GetAllProducts()
	{
		return _productRepository.GetAllProducts();
	}

	public Product? GetProductByIdForUser(Guid productId, long userId)
	{
		var prod = _productRepository.GetProductById(productId);
		if (prod == null || prod.UserId != userId)
		{
			Log.Warning("{@Method} - Product({@prod}) for user({@user}) not found.", nameof(GetProductByIdForUser), productId, userId);
			return default;
		}
		return prod;
	}

	public IEnumerable<Product> GetProductsForUser(long userId)
	{
		return _productRepository.GetAllProducts().Where(x => x.UserId == userId) ?? [];
	}
}
