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

		product.Id = id != default ? id : product.Id;

		product.Name = name ?? product.Name;

		product.UserId = userId != default ? userId : product.UserId;

		product.CurrentPrice = currentPrice != default ? currentPrice : product.CurrentPrice;

		product.OriginPrice = originPrice != default ? originPrice : product.OriginPrice;

		product.SalePercent = salePercent != default ? salePercent : product.SalePercent;

		product.OrinigLink = originLink ?? product.OrinigLink;

		product.CreatedAtDate = createdAt != default ? createdAt : product.CreatedAtDate;

		product.LastCheckedDate = lastCheckedAt != default ? lastCheckedAt : product.LastCheckedDate;

		product.OriginType = originType != OriginType.None ? originType : product.OriginType;

		return product;
	}

}
