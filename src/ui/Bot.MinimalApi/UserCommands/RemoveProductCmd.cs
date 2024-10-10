
using Bot.MinimalApi.Utils;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.MinimalApi.UserCommands;

public class RemoveProductCmd(long chatId, Guid pId, int messageId, TelegramBotClient bot, IProductService productService) : IUserCommand
{
	public long ChatId => chatId;

	public async Task<Message> ExecuteMeAsync()
	{
		bool result = await productService.DeleteProductOfUserAsync(ChatId, pId);

		var inlineKb = new InlineKeyboardMarkup();

		if (!result)
		{
			inlineKb.AddButton("Back", ConstantCommands.BACK_TO_LIST_CMD);

			return await bot.EditMessageTextAsync(
				chatId: ChatId,
				messageId: messageId,
				text: "Error when deleting.",
				replyMarkup: inlineKb);
		}

		// TODO - Create RESTORE function to restore the product. 
		// Means do not fully delete product, but only after returning Back to the List.

		inlineKb.AddButton("Restore (not working)", "restore")
			.AddNewRow()
			.AddButton("Back", ConstantCommands.BACK_TO_LIST_CMD);

		return await bot.EditMessageTextAsync(
			chatId: ChatId,
			messageId: messageId,
			text: "Product deleted.",
			replyMarkup: inlineKb);
	}
}
