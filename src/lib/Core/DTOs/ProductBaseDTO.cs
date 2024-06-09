using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs;

public class ProductBaseDTO
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
	/// Base price for the product.
	/// </summary>
	public decimal OriginPrice { get; set; }
}
