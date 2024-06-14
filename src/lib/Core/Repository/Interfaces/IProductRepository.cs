using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Interfaces;

/// <summary>
/// Internal interface representing CRUD operations to <see cref="Product"/> object.
/// </summary>
public interface IProductRepository
{
	/// <summary>
	/// Get all <see cref="Product"/>s.
	/// </summary>
	/// <returns><see cref="IEnumerable{Product}"/> list of all products.</returns>
	IEnumerable<Product> GetAllProducts();

	/// <summary>
	/// Get the <see cref="Product"/> by given id.
	/// </summary>
	/// <param name="id">Id to search.</param>
	/// <returns><see cref="Product"/> object if found, null if fail.</returns>
	Product? GetProductById(Guid productId);

	/// <summary>
	/// Create and add <see cref="Product"/> object into db.
	/// </summary>
	/// <param name="product">Object to add.</param>
	/// <returns><see cref="Product"/> object if succes, null if false.</returns>
	Task<Product?> CreateProductAsync(Product product);
	
	/// <summary>
	/// Update product object.
	/// </summary>
	/// <param name="product">Object to update.</param>
	/// <returns><see cref="Product"/> updated object if success, null if false.</returns>
	Task<Product?> UpdateProductAsync(Product product);

	/// <summary>
	/// Delete <see cref="Product"/> object by given Id.
	/// </summary>
	/// <param name="productId">Id of the product to delete.</param>
	/// <returns>bool value representing result.</returns>
	Task<bool> DeleteProductAsync(Guid productId);
}
