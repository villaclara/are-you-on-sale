using Core.EventArgs;
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.UserCommands;

public class RecheckSingleProductCmd(long chatId, Guid pId, ITrackProductService trackService, TelegramBotClient bot) : IUserCommand
{
	public long ChatId => chatId;

	private bool _isPChanged = false;

	public async Task<Message> ExecuteMeAsync()
	{
		trackService.ProductChanged += HandleProductChanged;

		await Task.Delay(1000);

		trackService.ProductChanged -= HandleProductChanged;

		if (!_isPChanged)
		{
			await bot.SendTextMessageAsync(ChatId, "Product has not changed.");
		}
		return null;
	}

	private async void HandleProductChanged(object? sender, ProductChangedEventArgs args)
	{
		_isPChanged = true;
	}
}
