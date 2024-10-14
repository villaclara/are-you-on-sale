using Bot.MinimalApi.Interfaces;
using Core.EventArgs;
using Core.Interfaces;
using Telegram.Bot;

namespace Bot.MinimalApi.Services;

public class RecheckProductTask(ITrackProductService track, TelegramBotClient bot) : IRecheckProductTask
{

	public ITrackProductService TrackProduct => track;

	public void Subscribe()
	{
		TrackProduct.ProductChanged += ProductChanged;
	}

	public void ProductChanged(object? sender, ProductChangedEventArgs args)
	{
		throw new NotImplementedException();
	}
}
