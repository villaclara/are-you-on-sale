using Core.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.UserCommands;

public class RestoreProductCmd(long chatId, Guid pId, int messageId, IProductService productService, TelegramBotClient bot) : IUserCommand
{
	public long ChatId => chatId;

	public async Task<Message> ExecuteMeAsync()
	{
		var cmd = new GetSingleProductCmd(ChatId, pId, messageId, productService, bot);

		return await cmd.ExecuteMeAsync();
	}
}
