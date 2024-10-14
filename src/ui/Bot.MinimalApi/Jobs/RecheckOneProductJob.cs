using Core.EventArgs;
using Core.Interfaces;
using Telegram.Bot;

namespace Bot.MinimalApi.Jobs;

public class RecheckOneProductJob
{
	private readonly ITrackProductService _trackService;
	private readonly TelegramBotClient _bot;
	private readonly IProductService _productService;
	private readonly Guid _pid;
	public RecheckOneProductJob(Guid pId, ITrackProductService track, TelegramBotClient client, IProductService productService)
	{
		_trackService = track;
		_trackService.ProductChanged += HandleProductChanged;

		_bot = client;
		_productService = productService;
		_pid = pId;
	}

	public async Task Recheck()
	{
		//var product = _productService.GetProductByIdForUser(_pid)
		//await _trackService.DoPriceCheckForSingleProductAsync(_pid);

		_trackService.ProductChanged -= HandleProductChanged;
	}

	private async void HandleProductChanged(object? sender, ProductChangedEventArgs args)
	{
		await _bot.SendTextMessageAsync(chatId: args.UserId, $"Product price changed: {args.ProductId}.");
	}
}
