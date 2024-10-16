using Core.Interfaces;
using Core.Services.ShopServices;
using Models.Entities;
using Models.Enums;

namespace Core.Services;

public class ProductBaseService : IProductBaseService
{
	public async Task<ProductBase?> GetProductBaseFromOriginAsync(OriginType originType, string originUrl)
	{
		return originType switch
		{
			OriginType.RZ => await RZ_Collector.GetProductBaseFromShopAsync(originUrl),
			_ => throw new Exception()
		};
	}

	public Task<ProductBase?> TryGetProductBaseFromAnyOriginAsync(string originUrl)
	{
		throw new NotImplementedException();
	}


}
