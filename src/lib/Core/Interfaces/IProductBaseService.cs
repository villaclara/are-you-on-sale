using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;

/// <summary>
/// Represents methods for parsing link and creating new <see cref="ProductBase"/> objects.
/// </summary>
public interface IProductBaseService
{
	/// <summary>
	/// Try automatically parse origin link without specifying <see cref="OriginType"/>.
	/// </summary>
	/// <param name="originUrl">Url to parse.</param>
	/// <returns><see cref="ProductBase"/> object if success, otherwise null.</returns>
	Task<ProductBase?> TryGetProductBaseFromAnyOrigin(string originUrl);

	/// <summary>
	/// Parse link with specifying the <see cref="OriginType"/> (shop).
	/// </summary>
	/// <param name="originType">One of <see cref="OriginType"/> enums.</param>
	/// <param name="originUrl">Url to parse.</param>
	/// <returns><see cref="ProductBase"/> object if success, otherwise null.</returns>
	Task<ProductBase?> GetProductBaseFromOrigin(OriginType originType, string originUrl);
}
