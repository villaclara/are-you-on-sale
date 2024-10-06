using Core.Interfaces;
using Core.Repository.Interfaces;
using Telegram.Bot;

namespace Bot.MinimalApi.UserCommands;

public class UserCommandFactory(
	IProductBaseService productBaseService,
	IProductService productService,
	IProductRepository productRepository,
	TelegramBotClient bot,
	IServiceProvider sp) : IUserCommandFactory
{
	public IUserCommand CreateUserCmd(string cmd, long userId)
	{
		//using var scope = sp.CreateScope();
		//var pService = scope.ServiceProvider.GetRequiredService<IProductService>();
		//var pBaseService = scope.ServiceProvider.GetRequiredService<IProductBaseService>();
		//var bot = scope.ServiceProvider.GetRequiredService<TelegramBotClient>();

		IUserCommand userCMD = cmd switch
		{
			"/add" => new AddProductCmd(),
			"/getall" => new GetAllProductsCmd(userId, productService, bot),
			_ => new UnknownCmd(userId, bot)
		};

		return userCMD;
	}


}
