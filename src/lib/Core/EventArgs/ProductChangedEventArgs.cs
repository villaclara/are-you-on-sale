using Models.Entities;
using Models.Enums;

namespace Core.EventArgs;

public class ProductChangedEventArgs(Product oldProd, Product? newProd, WhatProductFieldChanged productField) : System.EventArgs
{
	public Product OldProduct { get; set; } = oldProd;
	public Product? NewProduct { get; set; } = newProd;
	public WhatProductFieldChanged WhatFieldChanged { get; set; } = productField;
}
