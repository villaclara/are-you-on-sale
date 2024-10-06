using Bot.MinimalApi.Interfaces;
using Bot.MinimalApi.UserCommands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.Services;

public class BotUpdateHandler(ILogger<BotUpdateHandler> logger, IUserCommandFactory commandFactory) : IBotUpdateHandler
{
	private readonly ILogger<BotUpdateHandler> _logger = logger;
	private readonly IUserCommandFactory _commandFactory = commandFactory;

	public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
	{
		_logger.LogInformation($"HANDLE UPDATE. message - {update?.Message?.Text}, time - {update?.Message?.Date}");
		await (update switch
		{
			{ Message: { } message } => OnMessageReceived(message, botClient),
			_ => throw new NotImplementedException()
		});

	}

	private async Task OnMessageReceived(Message message, ITelegramBotClient bot)
	{
		if (message.Text is not { } messageText)
		{
			_logger.LogError("message.Text is not messageText");
			return;
		}
		if (message.From == null)
		{
			_logger.LogError("message.From is null");
			return;
		}

		IUserCommand cmd = _commandFactory.CreateUserCmd(messageText, message.From.Id);

		await cmd.ExecuteMeAsync();



	}



}
