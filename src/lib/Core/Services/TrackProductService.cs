using Core.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class TrackProductService : ITrackProductService
{
	public event Action<Product> ProductPriceChanged;

	public Task DoPriceCheckAllProducts()
	{
		throw new NotImplementedException();
	}

	public Task DoPriceCheckForSingleProduct(Product product)
	{
		throw new NotImplementedException();
	}
}
