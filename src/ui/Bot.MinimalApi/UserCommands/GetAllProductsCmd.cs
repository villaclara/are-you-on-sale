
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

		return await bot.SendTextMessageAsync(ChatId, products.Count().ToString());
		//bot.SendTextMessageAsync(_chatId, "getallcmd".ToString());
	}
}
