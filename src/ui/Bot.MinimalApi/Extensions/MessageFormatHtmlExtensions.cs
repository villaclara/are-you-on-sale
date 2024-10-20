using System.Text;
using Core.EventArgs;
using Models.Entities;
using Models.Enums;

namespace Bot.MinimalApi.Extensions;

public static class MessageFormatHtmlExtensions
{
	public static string ProductFullToMarkdownV2(this Product product)
	{
		var sb = new StringBuilder();
		sb.Append("Name: ").Append(@$"*{product.Name}*");


		return sb.ToString();
	}

	public static string ToHtml_ProductFull(this Product product)
	{
		var sb = new StringBuilder();
		sb.AppendLine(@$"<b>{product.Name}</b>")
			.AppendLine($@"Поточна ціна: <b>{product.CurrentPrice}</b> грн.")
			.AppendLine($@"Старт. ціна: <b>{product.OriginPrice}</b> грн.")
			.AppendLine($@"Ост. перевірка: <b>{product.LastCheckedDate}</b>")
			.AppendLine($@"Link: {product.OrinigLink}");

		return sb.ToString();

		//string text = "";
		//text += $@"<br>{product.Name}</br>";

		//return text;
	}

	public static string ToHtml_AllProductsToList(this IEnumerable<Product> products)
	{
		var sb = new StringBuilder();
		int index = 1;
		foreach (var product in products)
		{
			sb.AppendLine($@"{index}. {product.Name}");
			index++;
		}

		sb.AppendLine().AppendLine("Вибери товар:");

		return sb.ToString();
	}

	public static string To_Html_ProductChangedArgsToString(this ProductChangedEventArgs args)
	{
		string whatChanged = args.WhatFieldChanged switch
		{
			WhatProductFieldChanged.CurrentPrice =>
				@$"Current Price: Old - <b>{args.OldProduct.CurrentPrice}</b> грн. - New - <b>{args.NewProduct!.CurrentPrice}</b> грн.",
			WhatProductFieldChanged.OriginPrice =>
				@$"Base Price: Old - <b>{args.OldProduct.OriginPrice}</b> гре. - New - <b>{args.NewProduct!.OriginPrice}</b> грн.",
			WhatProductFieldChanged.All => "All",

			// Handles Error type also.
			_ => $"Link does not provide to {args.OldProduct.Name} anymore. Please delete it."
		};

		var sb = new StringBuilder();
		sb.AppendLine($@"[{DateTime.Now}]")
			.AppendLine($@"Product HAS Changed")
			.AppendLine()
			.AppendLine($@"<b>{args.OldProduct.Name}</b>")
			.AppendLine($@"{whatChanged}");

		return sb.ToString();
	}

}
