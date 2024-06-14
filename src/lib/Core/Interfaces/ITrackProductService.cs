using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;

/// <summary>
/// Contracts for performing tracking and notifying about any changes.
/// </summary>
public interface ITrackProductService
{
	
	/// <summary>
	/// Event is raised when the <see cref="ProductBase"/> price has been changed.
	/// </summary>
	event Action<Product> ProductPriceChanged;

	/// <summary>
	/// Get all products from db and perform actual price check, invoking <see cref="ProductPriceChanged"/> event in that case.
	/// </summary>
	/// <returns>Task.</returns>
	Task DoPriceCheckAllProducts();

	/// <summary>
	/// Do price check for one given <see cref="Product"/>.
	/// </summary>
	/// <param name="product">Object to check price for it.</param>
	/// <returns>Task.</returns>
	Task DoPriceCheckForSingleProduct(Product product);
}
