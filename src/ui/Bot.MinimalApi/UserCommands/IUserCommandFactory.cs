namespace Bot.MinimalApi.UserCommands;

public interface IUserCommandFactory
{
	IUserCommand CreateUserCmd(string cmd, long userId);
}
