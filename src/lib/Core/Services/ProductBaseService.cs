using Core.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

internal class ProductBaseService : IProductBaseService
{
	public Task<ProductBase?> GetProductBaseFromOriginAsync(OriginType originType, string originUrl)
	{
		throw new NotImplementedException();
	}

	public Task<ProductBase?> TryGetProductBaseFromAnyOriginAsync(string originUrl)
	{
		throw new NotImplementedException();
	}

	
}
