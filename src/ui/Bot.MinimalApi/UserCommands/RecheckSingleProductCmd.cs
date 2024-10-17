using Bot.MinimalApi.Extensions;
using Bot.MinimalApi.Utils;
using Core.EventArgs;
using Core.Interfaces;
using Models.Enums;
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

		await bot.DeleteMessageAsync(ChatId, messageId);

		var inlineKb = new InlineKeyboardMarkup()
				.AddButton(InlineKeyboardButton.WithCallbackData("Recheck", ConstantCommands.CHCK_PRODUCT_CMD + " " + args.OldProduct.Id))
				.AddButton(InlineKeyboardButton.WithCallbackData("Delete", ConstantCommands.DEL_SINGLE_PRODUCT_CMD + " " + args.OldProduct.Id))
				.AddNewRow()
				.AddButton(InlineKeyboardButton.WithCallbackData("Back", ConstantCommands.BACK_TO_LIST_EDIT_MSG_CMD));


		string whatChanged = args.WhatFieldChanged switch
		{
			WhatProductFieldChanged.CurrentPrice =>
				$"Current Price: Old - {args.OldProduct.CurrentPrice} - New - {args.NewProduct!.CurrentPrice}\n\n" + args.NewProduct!.ToHtml_ProductFull(),
			WhatProductFieldChanged.OriginPrice =>
				$"Base Price: Old - {args.OldProduct.OriginPrice} - New - {args.NewProduct!.OriginPrice}\n\n" + args.NewProduct!.ToHtml_ProductFull(),
			WhatProductFieldChanged.All => "All",

			// Handles Error type also.
			_ => $"Link does not provide to {args.OldProduct.Name} anymore. Please delete it."
		};

		string str = $"[{DateTime.Now}]\n"
			+ "Product HAS changed\n"
			+ whatChanged;

		await bot.SendTextMessageAsync(ChatId,
			str,
			parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
			replyMarkup: inlineKb);

	}

	private void Do(ProductChangedEventArgs args)
	{



	}
}
