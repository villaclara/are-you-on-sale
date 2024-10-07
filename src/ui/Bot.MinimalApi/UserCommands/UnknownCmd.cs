
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.UserCommands;

public class UnknownCmd(long chatId, ITelegramBotClient bot) : IUserCommand
{
	public long ChatId => chatId;
	public async Task<Message> ExecuteMeAsync()
	{
		return await bot.SendTextMessageAsync(ChatId, "Unknown command.");
	}

}
