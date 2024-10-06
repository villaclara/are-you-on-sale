
using Telegram.Bot;

namespace Bot.MinimalApi.UserCommands;

public class UnknownCmd(long chatId, ITelegramBotClient bot) : IUserCommand
{
	private readonly ITelegramBotClient _bot = bot;
	private readonly long _chatId = chatId;
	public async Task ExecuteMeAsync()
	{
		await _bot.SendTextMessageAsync(_chatId, "Unknown command.");
	}

}
