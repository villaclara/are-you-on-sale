using Telegram.Bot.Types;

namespace Bot.MinimalApi.UserCommands;

public interface IUserCommandFactory
{
	IUserCommand CreateUserCmd(Message message);
}
