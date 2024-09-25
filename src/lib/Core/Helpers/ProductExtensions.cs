using Models.Entities;
using Models.Enums;

namespace Core.Helpers;

public static class ProductExtensions
{

	/// <summary>
	/// Returns the same product instance but with new properties if they were provided (not null).
	/// </summary>
	/// <param name="product">Current instance.</param>
	/// <param name="id">New Guid if needed. Do not specify if do not want to change it.</param>
	/// <param name="name">New Name if needed. Do not specify if do not want to change it.</param>
	/// <param name="userId">New UserId if needed. Do not specify if do not want to change it.</param>
	/// <param name="currentPrice">New CurrentPrice if needed. Do not specify if do not want to change it.</param>
	/// <param name="originPrice">New OriginPrice if needed. Do not specify if do not want to change it.</param>
	/// <param name="salePercent">New SalePercent if needed. Do not specify if do not want to change it.</param>
	/// <param name="originLink">New OriginLink if needed. Do not specify if do not want to change it.</param>
	/// <param name="createdAt">New CreatedAt Date if needed. Do not specify if do not want to change it.</param>
	/// <param name="lastCheckedAt">New LastCheckedAt Date if needed. Do not specify if do not want to change it.</param>
	/// <param name="originType">New OriginType if needed. Do not specify if do not want to change it.</param>
	/// <returns></returns>
	public static Product NewProductWithUpdatedValues(this Product product,
		Guid id = default,
		string? name = null,
		long userId = default,
		decimal currentPrice = default,
		decimal originPrice = default,
		int salePercent = default,
		string? originLink = null,
		DateTime createdAt = default,
		DateTime lastCheckedAt = default,
		OriginType originType = OriginType.None
		)
	{
		if (id != default)
		{
			product.Id = id;
		}
		if (name != null)
		{
			product.Name = name;
		}
		if (userId != default)
		{
			product.UserId = userId;
		}
		if (currentPrice != default)
		{
			product.CurrentPrice = currentPrice;
		}
		if (originPrice != default)
		{
			product.OriginPrice = originPrice;
		}
		if (salePercent != default)
		{
			product.SalePercent = salePercent;
		}
		if (originLink != null)
		{
			product.OrinigLink = originLink;
		}
		if (createdAt != default)
		{
			product.CreatedAtDate = createdAt;
		}
		if (lastCheckedAt != default)
		{
			product.LastCheckedDate = lastCheckedAt;
		}
		if (originType != OriginType.None)
		{
			product.OriginType = originType;
		}
		return product;
	}

}
