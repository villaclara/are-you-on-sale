using Core.EventArgs;
using Core.Interfaces;

namespace Bot.MinimalApi.Interfaces;

public interface IRecheckProductTask
{
	ITrackProductService TrackProduct { get; }

	void ProductChanged(object sender, ProductChangedEventArgs args);
}
