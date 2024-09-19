using Core.Repository.Interfaces;
using DB.DB;
using Models.Entities;
using Serilog;

namespace Core.Repository.Services;

public class ProductRepository(ApplicationDBContext ctx) : IProductRepository
{
	private readonly ApplicationDBContext _ctx = ctx;

	public async Task<Product?> CreateProductAsync(Product product)
	{
		try
		{
			Log.Information("{@Method} - Try create product ({@product}).", nameof(CreateProductAsync), product);
			_ctx.Products.Add(product);
			await _ctx.SaveChangesAsync();
			return product;
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - Exception ({ex}).", nameof(CreateProductAsync), ex.Message);
			return null;
		}
	}

	public async Task<bool> DeleteProductAsync(Guid productId)
	{
		Log.Information("{@Method} - Try delete productId ({@productId}).", nameof(DeleteProductAsync), productId);
		var product = _ctx.Products.FirstOrDefault(p => p.Id == productId);
		if (product == null)
		{
			Log.Warning("{@Method} - No projects found. Return null", nameof(DeleteProductAsync));
			return false;
		}

		_ctx.Products.Remove(product);
		await _ctx.SaveChangesAsync();
		return true;
	}

	public IEnumerable<Product> GetAllProducts()
	{
		Log.Information("{@Method} - Try get all products.", nameof(GetAllProducts));
		return _ctx.Products.ToList() ?? [];
	}

	public Product? GetProductById(Guid productId)
	{
		Log.Information("{@Method} - Try get product by id({@id}).", nameof(GetProductById), productId);
		return _ctx.Products.FirstOrDefault(x => x.Id == productId);
	}

	public async Task<Product?> UpdateProductAsync(Product product)
	{
		try
		{
			Log.Information("{@Method} - Try update product ({@product}).", nameof(UpdateProductAsync), product);
			_ctx.Products.Update(product);
			await _ctx.SaveChangesAsync();

			return product;
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - Exception ({@ex}).", nameof(UpdateProductAsync), ex.Message);
			return null;
		}
	}
}
