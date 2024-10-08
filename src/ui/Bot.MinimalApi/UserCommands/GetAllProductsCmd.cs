
using Bot.MinimalApi.Extensions;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.UserCommands;

public class GetAllProductsCmd(long chatId, IProductService productService, ITelegramBotClient bot) : IUserCommand
{
	public long ChatId => chatId;
	public async Task<Message> ExecuteMeAsync()
	{
		var products = productService.GetProductsForUser(ChatId);

		return await bot.SendTextMessageAsync(ChatId, products.ToHtml_AllProductsToList(), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
		//bot.SendTextMessageAsync(_chatId, "getallcmd".ToString());
	}
}
