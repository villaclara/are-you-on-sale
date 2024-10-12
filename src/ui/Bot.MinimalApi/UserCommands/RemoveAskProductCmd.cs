
using Bot.MinimalApi.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.MinimalApi.UserCommands;

public class RemoveAskProductCmd(long chatId, Guid pId, int messageId, TelegramBotClient bot) : IUserCommand
{
	public long ChatId => chatId;

	public async Task<Message> ExecuteMeAsync()
	{

		var inlineKb = new InlineKeyboardMarkup();


		// TODO - create Manual price check button
		inlineKb.AddButton("Restore", ConstantCommands.RSTR_PRODUCT_CMD + " " + pId)
			.AddNewRow()
			.AddButton("Back", ConstantCommands.BACK_TO_LIST_EDIT_MSG_W_DEL_CMD + " " + pId);

		return await bot.EditMessageTextAsync(
			chatId: ChatId,
			messageId: messageId,
			text: "Product deleted.",
			replyMarkup: inlineKb);
	}
}
