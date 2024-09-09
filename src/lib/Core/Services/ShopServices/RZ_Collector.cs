using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services.ShopServices;

public static class RZ_Collector
{
	private static string _rztkApiLinkEmpty = @"https://rozetka.com.ua/api/product-api/v4/goods/get-main?goodsId=";

	public static ProductBase GetProductBaseFromShop(string url)
	{
		var productId = GetIdFromLink(url);

		using var client = new HttpClient();
		var response = client.GetStringAsync(_rztkApiLinkEmpty + productId).Result;

		if (response is null)
		{
			throw new Exception();
		}

		throw new Exception();

	}


	public static string GetIdFromLink(string url)
	{
		// URL TEMPLATE
		// https://rozetka.com.ua/ua/steelseries_62513/p179706829/
		var indexP = url.LastIndexOf('p');
		string str = url.Substring(indexP + 1); // 179706829/
		if(str.Last() == '/')
		{
			str = str.Remove(str.Length - 1);
		}
		return str;
	}


	public static string GetWithRegex(string source, string pattern)
	{
		var strs = Regex.Match(source, pattern);
		return strs.Value;
	}

	// REGEX for title
	// (\.*)(""title"": "".*"",""price"")

	// REGEX for price
	// (\.*)(""price"": ""[1-9]*"")

	// REGEX for old price
	// (\.*)(""old_price"": ""[1-9]*"")
	// Expected 
	// "old_price": "559"
}
