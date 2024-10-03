using Bot.MinimalApi.UserCommands;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.Services;

public class UpdateHandler : IUpdateHandler
{
	public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
	{
		Console.WriteLine("HANDLE UPDATE");
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
			"/addProduct" => new AddProductCmd(),
			"/getAll" => new GetAllProductsCmd(),
			_ => throw new NotImplementedException()
		});

		await cmd.ExecuteMeAsync();

	}
}
