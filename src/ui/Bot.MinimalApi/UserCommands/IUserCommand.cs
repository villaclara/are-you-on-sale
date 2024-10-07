namespace Bot.MinimalApi.UserCommands;


public interface IUserCommand
{
	long ChatId { get; }
	Task<Telegram.Bot.Types.Message> ExecuteMeAsync();
}
