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
internal interface IProductRepository
{
	/// <summary>
	/// Create and add <see cref="Product"/> object into db.
	/// </summary>
	/// <param name="product">Object to add.</param>
	/// <returns><see cref="Product"/> object if succes, null if false.</returns>
	Task<Product?> CreateProduct(Product product);
	
	/// <summary>
	/// Update product object.
	/// </summary>
	/// <param name="product">Object to update.</param>
	/// <returns><see cref="Product"/> updated object if success, null if false.</returns>
	Task<Product?> UpdateProduct(Product product);

	/// <summary>
	/// Delete <see cref="Product"/> object by given Id.
	/// </summary>
	/// <param name="productId">Id of the product to delete.</param>
	/// <returns>bool value representing result.</returns>
	Task<bool> DeleteProduct(int productId);
}
