using Core.Interfaces;
using Core.Repository.Interfaces;
using Models.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			CurrentPrice = product.OriginPrice,
			OrinigLink = product.OrinigLink,
			CreatedAtDate = DateTime.Now,
			LastCheckedDate = DateTime.Now,
			SalePercent = 0,
			UserId = userId
		};

		await _productRepository.CreateProductAsync(fullProduct);
	}

	public Task DeleteProductOfUserAsync(long userId, Guid productId)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Product> GetAllProducts()
	{
		throw new NotImplementedException();
	}

	public Product? GetProductByIdForUser(Guid productId, long userId)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Product> GetProductsForUser(long userId)
	{
		throw new NotImplementedException();
	}
}
