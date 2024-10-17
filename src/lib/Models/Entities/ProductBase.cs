using Models.Enums;

namespace Models.Entities;

/// <summary>
/// Represents simple product with base parameters, without assigned to any user.
/// </summary>
public class ProductBase
{
	/// <summary>
	/// Name of the product.
	/// </summary>
	public string Name { get; set; } = null!;

	/// <summary>
	/// Link where project were retrieved.
	/// </summary>
	public string OrinigLink { get; set; } = null!;

	/// <summary>
	/// Type of Shop for the product.
	/// </summary>
	public OriginType OriginType { get; set; }

	/// <summary>
	/// Base price for the product.
	/// </summary>
	public decimal OriginPrice { get; set; }

	/// <summary>
	/// Current price for the product in case there is ALREADY a sale.
	/// </summary>
	public decimal CurrentPrice { get; set; }
}
