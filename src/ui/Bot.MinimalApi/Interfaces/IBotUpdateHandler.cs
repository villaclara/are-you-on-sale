using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.Interfaces;

public interface IBotUpdateHandler
{
	Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct);
}
