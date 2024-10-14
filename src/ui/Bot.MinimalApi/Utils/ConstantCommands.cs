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
	public const string GET_SINGLE_PRODUCT_CMD = @"/q_get_e";

	/// <summary>
	/// The callback query command. Product id (GUID) should be appended at the end of command after space.
	/// </summary>
	public const string DEL_SINGLE_PRODUCT_CMD = @"/q_del";

	public const string BACK_TO_LIST_CMD = @"/q_back";

	/// <summary>
	/// The callback query command. To return back to list and Edit the current message.
	/// </summary>
	public const string BACK_TO_LIST_EDIT_MSG_CMD = @"/q_back_e";

	/// <summary>
	/// The callback query command. To return back to list and edit the current message WITH deleting the product.
	/// </summary>
	public const string BACK_TO_LIST_EDIT_MSG_W_DEL_CMD = @"/q_back_d";

	/// <summary>
	/// The callback query command. To restore product and display info about it and edit the current message.
	/// </summary>
	public const string RSTR_PRODUCT_CMD = @"/q_rstr_e";

	/// <summary>
	/// The callback query command. To check single product item and edit current message with actual info.
	/// </summary>
	public const string CHCK_PRODUCT_CMD = @"/q_chck_e";

	#endregion CallbackQuery Commands.
}
