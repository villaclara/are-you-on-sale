using Bot.MinimalApi.Extensions;
using Bot.MinimalApi.Utils;
using Core.EventArgs;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.MinimalApi.UserCommands;

public class RecheckSingleProductCmd(long chatId, Guid pId, int messageId, ITrackProductService trackService, TelegramBotClient bot, IProductService productService) : IUserCommand
{
	public long ChatId => chatId;

	private bool _isPChanged = false;

	public async Task<Message> ExecuteMeAsync()
	{
		// TODO Make it better. Maybe remove into separate class.
		trackService.ProductChanged += HandleProductChanged;

		var product = productService.GetProductByIdForUser(pId, ChatId);
		if (product != null)
		{
			await trackService.DoPriceCheckForSingleProductAsync(product);

			if (!_isPChanged)
			{
				var inlineKb = new InlineKeyboardMarkup()
				.AddButton(InlineKeyboardButton.WithCallbackData("Recheck", ConstantCommands.CHCK_PRODUCT_CMD + " " + product.Id))
				.AddButton(InlineKeyboardButton.WithCallbackData("Delete", ConstantCommands.DEL_SINGLE_PRODUCT_CMD + " " + product.Id))
				.AddNewRow()
				.AddButton(InlineKeyboardButton.WithCallbackData("Back", ConstantCommands.BACK_TO_LIST_EDIT_MSG_CMD));

				return await bot.EditMessageTextAsync(ChatId, messageId,
					$"[{DateTime.Now}]" + "\nProduct not chaged.\n\n" + product.ToHtml_ProductFull(), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
					replyMarkup: inlineKb);
			}
		}
		trackService.ProductChanged -= HandleProductChanged;

		return null;
	}

	private async void HandleProductChanged(object? sender, ProductChangedEventArgs args)
	{
		_isPChanged = true;

		//await bot.DeleteMessageAsync(ChatId, messageId);

		var inlineKb = new InlineKeyboardMarkup()
				.AddButton(InlineKeyboardButton.WithUrl("Web", args.OldProduct.OrinigLink));

		await bot.SendTextMessageAsync(ChatId,
			args.To_Html_ProductChangedArgsToString(),
			parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
			replyMarkup: inlineKb);

	}

}
