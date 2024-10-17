namespace Models.Entities;

/// <summary>
/// Represents product object with assigned user and additional parameters.
/// </summary>
public class Product : ProductBase, ICloneable
{
	/// <summary>
	/// Id for db.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Represents userId assigned to product (ChatId).
	/// </summary>
	public long UserId { get; set; }

	/// <summary>
	/// Price of product for a time of <see cref="LastCheckedDate"/>.
	/// </summary>
	public new decimal CurrentPrice { get; set; }

	/// <summary>
	/// Difference in percent between <see cref="ProductBase.OriginPrice"/> and <see cref="Product.CurrentPrice"/>.
	/// </summary>
	public int SalePercent { get; set; }

	/// <summary>
	/// Date last check of product price was performed.
	/// </summary>
	public DateTime LastCheckedDate { get; set; }

	/// <summary>
	/// Date when the product was created and added to storage.
	/// </summary>
	public DateTime CreatedAtDate { get; set; }

	public object Clone()
	{
		return MemberwiseClone();
	}
}
