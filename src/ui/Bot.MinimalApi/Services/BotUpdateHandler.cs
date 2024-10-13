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
		await (update switch
		{
			{ Message: { } message } => OnMessageReceived(message, botClient),
			{ CallbackQuery: { } callbackQuery } => OnCallbackQueryReceived(callbackQuery, botClient),
			_ => UnknownUpdate(update, botClient)
		});
	}



	private async Task OnMessageReceived(Message message, ITelegramBotClient bot)
	{
		try
		{

			_logger.LogInformation("Message HANDLE - {message.Text}, {message.Date}", message.Text, message.Date);

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

			IUserCommand cmd = _commandFactory.CreateUserCmdFromMessage(message);

			await cmd.ExecuteMeAsync();

		}
		catch (Exception ex)
		{
			_logger.LogError("{Method} - Ex. {Exception}", nameof(OnMessageReceived), ex.Message);
			await bot.SendTextMessageAsync(message.Chat.Id, "Помилка, спробуй ще раз.");
		}


	}

	private async Task OnCallbackQueryReceived(CallbackQuery callbackQuery, ITelegramBotClient bot)
	{
		try
		{

			_logger.LogInformation("CallbackQuery HANDLE - {callbackQuery.Data}", callbackQuery.Data);

			// Necessary to react to callback by library. We will show nothing.
			await bot.AnswerCallbackQueryAsync(callbackQuery.Id);

			if (callbackQuery.Data == null || callbackQuery.Message == null)
			{
				_logger.LogError("callbackquery is empty.");
				return;
			}

			IUserCommand cmd = _commandFactory.CreateUserCmdFromCallbackQuery(callbackQuery);

			await cmd.ExecuteMeAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError("{Method} - Ex. {Exception}", nameof(OnMessageReceived), ex.Message);
			await bot.SendTextMessageAsync(callbackQuery.From.Id, "Помилка, спробуй ще раз.");
		}
	}

	private Task UnknownUpdate(Update update, ITelegramBotClient botClient)
	{
		_logger.LogError("Unknown update: {UpdateType}.", update.Type);
		return Task.CompletedTask;
	}



}
