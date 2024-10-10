using System.Text;
using Models.Entities;

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
			.AppendLine($@"Поточна ціна: <b>{product.CurrentPrice}</b>")
			.AppendLine($@"Старт. ціна: <b>{product.OriginPrice}</b>")
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
}
