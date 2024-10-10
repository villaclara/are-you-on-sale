using Telegram.Bot.Types;

namespace Bot.MinimalApi.UserCommands;

public interface IUserCommandFactory
{
	IUserCommand CreateUserCmdFromMessage(Message message);
	IUserCommand CreateUserCmdFromCallbackQuery(CallbackQuery query);
}
