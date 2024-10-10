namespace Bot.MinimalApi.Utils;

public class ConstantCommands
{
	#region Default Commands.
	public const string GET_ALL_PRODUCTS_CMD = @"/getall";
	public const string ADD_NEW_PRODUCT_CMD = @"/add";

	/// <summary>
	/// The command to start adding new product by bot. It should be appended with link (https://...) after space.
	/// </summary>
	public const string ADD_NEW_PRODUCT_LINK_SENT_CMD = @"/new";

	#endregion Default Commands.


	#region CallbackQuery Commands. 
	// ALL CALLBACKQUERY COMMANDS SHOULD START WITH "/q_" symbol

	/// <summary>
	/// The callback query command. Product id (GUID) should be appended at the end of command after space.
	/// </summary>
	public const string GET_SINGLE_PRODUCT_CMD = @"/q_get";

	/// <summary>
	/// The callback query command. Product id (GUID) should be appended at the end of command after space.
	/// </summary>
	public const string DEL_SINGLE_PRODUCT_CMD = @"/q_del";

	public const string BACK_TO_LIST_CMD = @"/q_back";

	#endregion CallbackQuery Commands.
}
