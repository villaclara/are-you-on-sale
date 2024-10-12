using Bot.MinimalApi.Extensions;
using Bot.MinimalApi.Utils;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.MinimalApi.UserCommands;

public class GetSingleProductCmd(long chatId, Guid pId, int messageId, IProductService productService, TelegramBotClient bot) : IUserCommand
{
	public long ChatId => chatId;
	public Guid PId => pId;

	public async Task<Message> ExecuteMeAsync()
	{
		var product = productService.GetProductByIdForUser(PId, ChatId);

		var inlineKb = new InlineKeyboardMarkup();

		if (product == null)
		{
			inlineKb.AddButton("Back", ConstantCommands.BACK_TO_LIST_CMD);
			return await bot.EditMessageTextAsync(
				chatId: ChatId,
				messageId: messageId,
				text: "Error when loading product info. Try again",
				replyMarkup: inlineKb);
		}

		inlineKb.AddButton(InlineKeyboardButton.WithCallbackData("Delete", ConstantCommands.DEL_SINGLE_PRODUCT_CMD + " " + product.Id))
			.AddNewRow()
			.AddButton(InlineKeyboardButton.WithCallbackData("Back", ConstantCommands.BACK_TO_LIST_EDITMSG_CMD));

		return await bot.EditMessageTextAsync(
			chatId: ChatId,
			messageId: messageId,
			text: product.ToHtml_ProductFull(),
			parseMode: ParseMode.Html,
			replyMarkup: inlineKb);

	}
}
