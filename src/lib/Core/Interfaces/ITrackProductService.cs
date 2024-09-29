using Core.EventArgs;
using Models.Entities;

namespace Core.Interfaces;

/// <summary>
/// Contracts for performing tracking and notifying about any changes.
/// </summary>
public interface ITrackProductService
{

	/// <summary>
	/// Event is raised when the <see cref="ProductBase"/> price has been changed.
	/// </summary>
	event EventHandler<ProductChangedEventArgs> ProductChanged;

	/// <summary>
	/// Get all products from db and perform actual price check, invoking <see cref="ProductPriceChanged"/> event in that case.
	/// </summary>
	/// <returns>Task.</returns>
	Task DoPriceCheckAllProductsAsync();

	/// <summary>
	/// Do price check for one given <see cref="Product"/>.
	/// </summary>
	/// <param name="product">Object to check price for it.</param>
	/// <returns>Task.</returns>
	Task DoPriceCheckForSingleProductAsync(Product product);
}
