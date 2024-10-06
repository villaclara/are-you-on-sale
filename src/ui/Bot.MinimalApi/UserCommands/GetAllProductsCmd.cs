
using Core.Interfaces;
using Telegram.Bot;

namespace Bot.MinimalApi.UserCommands;

public class GetAllProductsCmd(long chatId, IProductService productService, ITelegramBotClient bot) : IUserCommand
{
	private readonly IProductService _productService = productService;
	private readonly ITelegramBotClient _bot = bot;
	private readonly long _chatId = chatId;
	public async Task ExecuteMeAsync()
	{
		var products = productService.GetProductsForUser(_chatId);

		await bot.SendTextMessageAsync(_chatId, products.Count().ToString());
		//bot.SendTextMessageAsync(_chatId, "getallcmd".ToString());
	}
}
