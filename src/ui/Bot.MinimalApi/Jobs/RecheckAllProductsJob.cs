using Coravel.Invocable;
using Core.EventArgs;
using Core.Interfaces;
using Telegram.Bot;

namespace Bot.MinimalApi.Jobs;

public class RecheckAllProductsJob : IInvocable
{
	private readonly ITrackProductService _trackService;
	private readonly TelegramBotClient _bot;
	private readonly IProductService _productService;
	private readonly ILogger<RecheckAllProductsJob> _logger;

	public RecheckAllProductsJob(ITrackProductService track, TelegramBotClient client, IProductService productService, ILogger<RecheckAllProductsJob> logger)
	{
		_logger = logger;
		_trackService = track;
		_trackService.ProductChanged += HandleProductChanged;

		_bot = client;
		_productService = productService;
	}

	public async Task Invoke()
	{
		// TODO Make it better
		_logger.LogInformation("Invoke from RecheckAllProducts CALL at {datetime}", DateTime.Now);
		await _trackService.DoPriceCheckAllProductsAsync();

		_trackService.ProductChanged -= HandleProductChanged;

		_logger.LogInformation("Invoke from RecheckAllProducts END at {datetime}", DateTime.Now);

	}


	private async void HandleProductChanged(object? sender, ProductChangedEventArgs args)
	{
		//await _bot.SendTextMessageAsync(chatId: args.UserId, $"Product price changed: {args.Product.Name}, Old {args.OldValue}, New {args.NewValue}.");
	}
}
