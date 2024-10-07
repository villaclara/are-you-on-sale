
using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.UserCommands;

public class RemoveProductCmd(long chatId, TelegramBotClient bot, IProductService productService) : IUserCommand
{
	public long ChatId => chatId;

	public Task<Message> ExecuteMeAsync()
	{
		throw new NotImplementedException();
	}
}
