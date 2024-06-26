﻿using Core.Interfaces;
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
		if(product == null)
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
		throw new NotImplementedException();
	}

	public Product? GetProductById(Guid productId)
	{
		throw new NotImplementedException();
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
