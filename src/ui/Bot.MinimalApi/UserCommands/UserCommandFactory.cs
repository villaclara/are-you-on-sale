using Core.Interfaces;
using Core.Repository.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.MinimalApi.UserCommands;

public class UserCommandFactory(
	IProductBaseService productBaseService,
	IProductService productService,
	IProductRepository productRepository,
	TelegramBotClient bot,
	IServiceProvider sp) : IUserCommandFactory
{
	public IUserCommand CreateUserCmd(Message message)
	{
		//using var scope = sp.CreateScope();
		//var pService = scope.ServiceProvider.GetRequiredService<IProductService>();
		//var pBaseService = scope.ServiceProvider.GetRequiredService<IProductBaseService>();
		//var bot = scope.ServiceProvider.GetRequiredService<TelegramBotClient>();

		if (message.Text.StartsWith("https"))
		{
			message.Text = "new " + message.Text;
		}

		IUserCommand userCMD = message.Text!.Split(" ")[0] switch
		{
			"/add" => new AddProductCmd(message.From!.Id, message.Text, bot, productBaseService, productService),
			"/getall" => new GetAllProductsCmd(message.From!.Id, productService, bot),
			"new" => new AddProductCmd(message.From.Id, message.Text.Split(" ")[1], bot, productBaseService, productService),
			_ => new UnknownCmd(message.From!.Id, bot)
		};

		return userCMD;
	}


}
