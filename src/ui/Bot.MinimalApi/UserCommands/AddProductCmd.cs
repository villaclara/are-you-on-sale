
using Bot.MinimalApi.Extensions;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.UserCommands;

public class AddProductCmd(long chatId, string message, TelegramBotClient bot, IProductBaseService productBaseService, IProductService productService) : IUserCommand
{

	public long ChatId => chatId;
	public async Task<Message> ExecuteMeAsync()
	{
		if (message.StartsWith("https"))
		{
			return await AddProductToUserAsync(message);
		}

		return await bot.SendTextMessageAsync(ChatId, "Please provide link.");
	}

	private async Task<Message> AddProductToUserAsync(string url)
	{
		var product = await productBaseService.GetProductBaseFromOriginAsync(Models.Enums.OriginType.RZ, url);

		if (product == null)
		{
			return await bot.SendTextMessageAsync(ChatId, "Failed to get the link, please provide correct one.");
		}

		var added = await productService.AddProductToUserAsync(ChatId, product);

		if (added == null)
		{
			return await bot.SendTextMessageAsync(ChatId, $"Error when adding product. Try again.");
		}


		return await bot.SendTextMessageAsync(ChatId, added.ToHtml_ProductFull(), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
	}
}

