using Bot.MinimalApi.Interfaces;
using Bot.MinimalApi.UserCommands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.Services;

public class BotUpdateHandler(ILogger<BotUpdateHandler> logger) : IBotUpdateHandler
{
	private readonly ILogger<BotUpdateHandler> _logger = logger;

	public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
	{
		_logger.LogInformation("HANDLE UPDATE");
		await (update switch
		{
			{ Message: { } message } => OnMessageReceived(message),
			_ => throw new NotImplementedException()
		});

	}

	private async Task OnMessageReceived(Message message)
	{
		if (message.Text is not { } messageText)
			return;

		IUserCommand cmd = (messageText.Split(' ')[0] switch
		{
			"/add" => new AddProductCmd(),
			"/start" => new GetAllProductsCmd(),
			_ => throw new NotImplementedException()
		});

		await cmd.ExecuteMeAsync();

	}
}
