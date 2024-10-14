using Core.EventArgs;
using Core.Interfaces;
using Telegram.Bot;

namespace Bot.MinimalApi.Jobs;

public class RecheckAllProductsJob
{
	private readonly ITrackProductService _trackService;
	private readonly TelegramBotClient _bot;
	public RecheckAllProductsJob(ITrackProductService track, TelegramBotClient client)
	{
		_trackService = track;
		_trackService.ProductChanged += HandleProductChanged;

		_bot = client;
	}

	public async Task Recheck()
	{
		await _trackService.DoPriceCheckAllProductsAsync();

		_trackService.ProductChanged -= HandleProductChanged;
	}

	private async void HandleProductChanged(object? sender, ProductChangedEventArgs args)
	{
		await _bot.SendTextMessageAsync(chatId: args.UserId, $"Product price changed: {args.ProductId}.");
	}
}
