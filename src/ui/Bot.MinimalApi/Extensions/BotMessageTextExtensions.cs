using Core.EventArgs;
using Models.Enums;

namespace Bot.MinimalApi.Extensions;

public static class BotMessageTextExtensions
{
	public static string MakeTextProductChangedFromArgs(this ProductChangedEventArgs args)
	{
		string whatChanged = args.WhatFieldChanged switch
		{
			WhatProductFieldChanged.CurrentPrice =>
				$"Current Price: Old - {args.OldProduct.CurrentPrice} - New - {args.NewProduct!.CurrentPrice}\n\n",
			WhatProductFieldChanged.OriginPrice =>
				$"Base Price: Old - {args.OldProduct.OriginPrice} - New - {args.NewProduct!.OriginPrice}\n\n",
			WhatProductFieldChanged.All => "All",

			// Handles Error type also.
			_ => $"Link does not provide to {args.OldProduct.Name} anymore. Please delete it."
		};

		string str = $"[{DateTime.Now}]\n"
			+ "Product HAS changed\n"
			+ args.OldProduct.Name + "\n"
			+ whatChanged;

		return str;
	}
}
