
namespace Bot.MinimalApi.UserCommands;

public class AddProductCmd : IUserCommand
{

	public Task ExecuteMeAsync()
	{
		Console.WriteLine("ADDPRODUCTCMD");
		return Task.CompletedTask;
	}
}

