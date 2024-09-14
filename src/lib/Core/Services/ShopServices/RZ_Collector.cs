using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services.ShopServices;

public static class RZ_Collector
{
	private static string _rztkApiLinkEmpty = @"https://rozetka.com.ua/api/product-api/v4/goods/get-main?goodsId=";

	public static async Task<ProductBase> GetProductBaseFromShopAsync(string url)
	{
		var productId = GetIdFromLink(url);

		using var client = new HttpClient();
		var response = await client.GetStringAsync(_rztkApiLinkEmpty + productId);

		if (response is null)
		{
			throw new HttpRequestException("Failed to get data from URL.");
		}




		try
		{
			var title = GetWithRegex(response, @"(\.*""title"".*)(?=,""price"":"".*"",""old_price)"); // "title":"Миша SteelSeries Rival 3 USB Black (SS62513)"
			var price = GetWithRegex(response, @"(\.*)(""price"":""[1-9]*"")(?=,""old_price)");     // "price":"1699"
			var old_price = GetWithRegex(response, @"(\.*)(""old_price"":""[1-9]*"")(?=,""price_pcs)");     // "old_price":"1699"

			string titleCut = title[9..^1];
			string priceCut = price[9..^1]; // same as .Remove(priceCut.Length - 1) or .Substring(0, priceCut.Length - 1)
			string oldPriceCut = old_price[13..^1];

			int.TryParse(priceCut, out int priceInt);
			int.TryParse(oldPriceCut, out int old_priceInt);

			return new ProductBase()
			{
				Name = titleCut,
				OrinigLink = url,
				OriginPrice = old_priceInt,
				CurrentPrice = priceInt
			};
		}
		catch (Exception ex)
		{
			throw new Exception("Failed to read data and return object." + ex);
		}

	}


	public static string GetIdFromLink(string url)
	{
		// URL TEMPLATE
		// https://rozetka.com.ua/ua/steelseries_62513/p179706829/
		var indexP = url.LastIndexOf('p');
		string str = url[(indexP + 1)..]; // 179706829/
		if(str.Last() == '/')
		{
			str = str.Remove(str.Length - 1);
		}
		return str;
	}


	public static string GetWithRegex(string source, string pattern)
	{
		var strs = Regex.Match(source, pattern);

		if(!strs.Success)
		{
			throw new Exception("no matches for regex");
		}

		return strs.Value;
	}

	// REGEX for title
	// (\.*""title"".*)(?=,""price"":"".*"",""old_price)
	// Expected
	// "title":"Миша SteelSeries Rival 3 USB Black (SS62513)"

	// REGEX for price
	// (\.*)(""price"":""[1-9]*"")(?=,""old_price)
	// Expected
	// "price":"1699"

	// REGEX for old price
	// (\.*)(""old_price"":""[1-9]*"")(?=,""price_pcs)
	// Expected 
	// "old_price":"1699"

}
