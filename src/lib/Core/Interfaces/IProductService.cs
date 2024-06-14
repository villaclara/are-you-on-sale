using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;

/// <summary>
/// Main contracts for managing products for user.
/// </summary>
public interface IProductService
{
	/// <summary>
	/// Service responsible to perform tracking and notify if price has changed.
	/// </summary>
	ITrackProductService TrackProductService { get; }

	/// <summary>
	/// Get list of <see cref="Product"/> for User.
	/// </summary>
	/// <param name="userId">Id of user.</param>
	/// <returns><see cref="IEnumerable{T}"/> of <see cref="Product"/> objects.</returns>
	IEnumerable<Product> GetProductsForUser(long userId);

	/// <summary>
	/// Gets the specified by Id <see cref="Product"/> object for specific userId.
	/// </summary>
	/// <param name="productId"><see cref="Guid"/> of the product.</param>
	/// <param name="userId">Id of user.</param>
	/// <returns><see cref="Product"/> object if found and if the product is assigned to <paramref name="userId"/>, null otherwise.</returns>
	Product? GetProductByIdForUser(Guid productId, long userId);

	/// <summary>
	/// Get list of all <see cref="Product"/>.
	/// </summary>
	/// <returns><see cref="IEnumerable{T}"/> of <see cref="Product"/> objects.</returns>
	IEnumerable<Product> GetAllProducts();

	/// <summary>
	/// Creates the <see cref="Product"/> from <see cref="ProductBase"/> object and assign to userId.
	/// </summary>
	/// <param name="userId">Id of user to add product.</param>
	/// <param name="product"><see cref="ProductBase"/> object to add.</param>
	/// <returns>Task.</returns>
	Task AddProductToUserAsync(long  userId, ProductBase product);

	/// <summary>
	/// Deletes the selected <see cref="Product"/> object from userId.
	/// </summary>
	/// <param name="userId">Id of user to delete product.</param>
	/// <param name="productId"><see cref="Product"/> object to remove from db.</param>
	/// <returns>Task.</returns>
	Task DeleteProductOfUserAsync(long userId, Guid productId);
}
