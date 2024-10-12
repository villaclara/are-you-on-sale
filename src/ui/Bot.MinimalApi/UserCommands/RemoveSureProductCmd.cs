using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.UserCommands;


/// <summary>
/// The command should only be called when returning back from deleting the <see cref="Models.Entities.Product"/> instance.
/// </summary>
/// <param name="chatId"></param>
/// <param name="pId"></param>
/// <param name="messageId"></param>
/// <param name="bot"></param>
/// <param name="productService"></param>
public class RemoveSureProductCmd(long chatId, Guid pId, int messageId, TelegramBotClient bot, IProductService productService) : IUserCommand
{
	public long ChatId => chatId;

	public async Task<Message> ExecuteMeAsync()
	{
		bool result = await productService.DeleteProductOfUserAsync(ChatId, pId);

		if (result)
		{
			var cmd = new GetAllProductsCmd(ChatId, productService, bot, editCurrentMessage: true, messageId: (int)messageId);
			return await cmd.ExecuteMeAsync();
		}

		var cmd1 = new GetSingleProductCmd(ChatId, pId, messageId, productService, bot);
		return await cmd1.ExecuteMeAsync();

		//var inlineKb = new InlineKeyboardMarkup();

		//if (!result)
		//{
		//	inlineKb.AddButton("Back", ConstantCommands.BACK_TO_LIST_EDIT_MSG_CMD);

		//	return await bot.EditMessageTextAsync(
		//		chatId: ChatId,
		//		messageId: messageId,
		//		text: "Error when deleting.",
		//		replyMarkup: inlineKb);
		//}

		//// TODO - Create RESTORE function to restore the product. 
		//// Means do not fully delete product, but only after returning Back to the List.

		//inlineKb.AddButton("Restore (not working)", "restore")
		//	.AddNewRow()
		//	.AddButton("Back", ConstantCommands.BACK_TO_LIST_EDIT_MSG_CMD);

		//return await bot.EditMessageTextAsync(
		//	chatId: ChatId,
		//	messageId: messageId,
		//	text: "Product deleted.",
		//	replyMarkup: inlineKb);
	}
}
