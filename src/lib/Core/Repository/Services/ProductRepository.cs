using Core.Interfaces;
using Core.Repository.Interfaces;
using Models.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Services;

internal class ProductRepository(IApplicationDBContext ctx) : IProductRepository
{
	private readonly IApplicationDBContext _ctx = ctx;

	public async Task<Product?> CreateProduct(Product product)
	{
		try
		{
			Log.Information("{@Method} - Try create product ({@product}).", nameof(CreateProduct), product);
			_ctx.Products.Add(product);
			await _ctx.SaveChangesAsync();
			return product;
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - Exception ({ex}).", nameof(CreateProduct), ex.Message);
			return null;
		}
	}

	public async Task<bool> DeleteProduct(int productId)
	{
		Log.Information("{@Method} - Try delete productId ({@productId}).", nameof(DeleteProduct), productId);
		var product = _ctx.Products.FirstOrDefault(p => p.Id == productId);
		if(product == null)
		{
			Log.Warning("{@Method} - No projects found. Return null", nameof(DeleteProduct));
			return false;
		}

		_ctx.Products.Remove(product);
		await _ctx.SaveChangesAsync();
		return true;
	}

	public async Task<Product?> UpdateProduct(Product product)
	{
		try
		{
			Log.Information("{@Method} - Try update product ({@product}).", nameof(UpdateProduct), product);
			_ctx.Products.Update(product);
			await _ctx.SaveChangesAsync();
			return product;
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - Exception ({@ex}).", nameof(UpdateProduct), ex.Message);
			return null;
		}
	}
}
