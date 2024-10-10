
using Bot.MinimalApi.Extensions;
using Bot.MinimalApi.Utils;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.MinimalApi.UserCommands;

public class GetAllProductsCmd(long chatId, IProductService productService, ITelegramBotClient bot) : IUserCommand
{
	public long ChatId => chatId;
	public async Task<Message> ExecuteMeAsync()
	{
		var products = productService.GetProductsForUser(ChatId);

		var inlineKb = new InlineKeyboardMarkup();

		int index = 1;
		foreach (var product in products)
		{
			// Allow only 7 buttons to be present in one row. When there is more products then create new row of buttons.
			if (index % 8 == 0)
			{
				inlineKb.AddNewRow();
			}
			inlineKb.AddButton(InlineKeyboardButton.WithCallbackData($"{index}", ConstantCommands.GET_SINGLE_PRODUCT_CMD + " " + product.Id.ToString()));
			index++;
		}

		return await bot.SendTextMessageAsync(ChatId, products.ToHtml_AllProductsToList(), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: inlineKb);
		//bot.SendTextMessageAsync(_chatId, "getallcmd".ToString());
	}
}
