using Plugin.Maui.DebugRainbows;

namespace Plugin.Maui.DebugRainbows.Sample;

public partial class MainPage : ContentPage
{
	readonly IFeature feature;

	public MainPage(IFeature feature)
	{
		InitializeComponent();
		
		this.feature = feature;
	}
}
