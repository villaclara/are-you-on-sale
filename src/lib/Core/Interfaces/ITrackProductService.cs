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
	IProductBaseService ProductBaseService { get; }
	
	event Action ProductPriceChanged;

	Task OnTrackRequestReceived();

	Task TrackProductManually(Product product);
}
