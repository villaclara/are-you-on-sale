using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Core;

public interface IApplicationDBContext
{
	DbSet<ProductBase> Products { get; }
}
