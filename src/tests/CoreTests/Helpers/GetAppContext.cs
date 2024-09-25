using DB.DB;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace CoreTests.Helpers;

public static class GetAppContext
{
	public static async Task<ApplicationDbContext> GetAppContextDbInMemoryAsync()
	{
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

		var ctx = new ApplicationDbContext(options);
		ctx.Database.EnsureCreated();

		if (!await ctx.Products.AnyAsync())
		{
			await ctx.AddRangeAsync(
				new[]
				{
					new Product()
					{
						Name = @"TestName1",
						OrinigLink = "https://testLink1.com",
						OriginPrice = 100,
						CurrentPrice = 200,
						CreatedAtDate = DateTime.Parse("2024-09-20 01:43:45.434223+03").ToUniversalTime(),
						Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
						LastCheckedDate = DateTime.Parse("2024-09-20 01:43:45.434265+03").ToUniversalTime(),
						OriginType = Models.Enums.OriginType.RZ,
						SalePercent = 50,
						UserId = 2
					},
					new Product()
					{
						Name = @"TestName2",
						OrinigLink = "https://testLink2.com",
						OriginPrice = 100,
						CurrentPrice = 150,
						CreatedAtDate = DateTime.Parse("2024-09-20 01:43:45.434223+03").ToUniversalTime(),
						Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
						LastCheckedDate = DateTime.Parse("2024-09-20 01:43:45.434265+03").ToUniversalTime(),
						OriginType = Models.Enums.OriginType.RZ,
						SalePercent = 66,
						UserId = 2
					},
					new Product()
					{
						Name = @"TestName3",
						OrinigLink = "https://testLink3.com",
						OriginPrice = 100,
						CurrentPrice = 100,
						CreatedAtDate = DateTime.Parse("2024-09-20 01:43:45.434223+03").ToUniversalTime(),
						Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
						LastCheckedDate = DateTime.Parse("2024-09-20 01:43:45.434265+03").ToUniversalTime(),
						OriginType = Models.Enums.OriginType.RZ,
						SalePercent = 0,
						UserId = 1
					},
				});

			await ctx.SaveChangesAsync();
		}

		return ctx;
	}
}
