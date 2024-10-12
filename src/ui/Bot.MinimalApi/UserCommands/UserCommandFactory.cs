using Bot.MinimalApi.Utils;
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
	public IUserCommand CreateUserCmdFromMessage(Message message)
	{
		//using var scope = sp.CreateScope();
		//var pService = scope.ServiceProvider.GetRequiredService<IProductService>();
		//var pBaseService = scope.ServiceProvider.GetRequiredService<IProductBaseService>();
		//var bot = scope.ServiceProvider.GetRequiredService<TelegramBotClient>();

		if (message.Text!.StartsWith("https"))
		{
			message.Text = ConstantCommands.ADD_NEW_PRODUCT_LINK_SENT_CMD + " " + message.Text; // "new https://test.link"
		}

		IUserCommand userCMD = message.Text!.Split(" ")[0] switch
		{
			// "/add"
			ConstantCommands.ADD_NEW_PRODUCT_CMD => new AddProductCmd(
				chatId: message.From!.Id,
				message: message.Text,
				bot,
				productBaseService,
				productService),

			// "/getall"
			ConstantCommands.GET_ALL_PRODUCTS_CMD => new GetAllProductsCmd(
				chatId: message.From!.Id,
				productService,
				bot),

			// "/new"
			ConstantCommands.ADD_NEW_PRODUCT_LINK_SENT_CMD => new AddProductCmd(
				chatId: message.From!.Id,
				message: message.Text.Split(" ")[1],
				bot,
				productBaseService,
				productService),

			_ => new UnknownCmd(message.From!.Id, bot)
		};


		return userCMD;
	}

	public IUserCommand CreateUserCmdFromCallbackQuery(CallbackQuery query)
	{
		IUserCommand userCMD = query.Data!.Split(" ")[0] switch
		{
			// "/q_get"
			ConstantCommands.GET_SINGLE_PRODUCT_CMD => new GetSingleProductCmd(
				chatId: query.From.Id,
				pId: Guid.Parse(query.Data.Split(" ")[1]),
				messageId: query.Message!.MessageId,
				productService,
				bot),


			// TODO - Edit message when received BACK command query instead of sending new one

			// "/q_back_e"
			ConstantCommands.BACK_TO_LIST_EDITMSG_CMD => new GetAllProductsCmd(
				chatId: query.From.Id,
				productService,
				bot,
				editCurrentMessage: true,
				messageId: query.Message!.MessageId),


			// "/q_del"
			ConstantCommands.DEL_SINGLE_PRODUCT_CMD => new RemoveProductCmd(
				chatId: query.From.Id,
				pId: Guid.Parse(query.Data.Split(" ")[1]),
				messageId: query.Message!.MessageId,
				bot,
				productService),

			_ => throw new NotImplementedException()
		};

		return userCMD;
	}



}
