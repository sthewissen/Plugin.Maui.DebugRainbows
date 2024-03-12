using Plugin.Maui.DebugRainbowsModels;

namespace Plugin.Maui.DebugRainbows.Controls;

public class DebugGrid : ContentView
{
    public double HorizontalItemSize { get; set; } = 20;
    public double VerticalItemSize { get; set; } = 20;
    public int MajorGridLineInterval { get; set; } = 4;
    public GridLineOptions MajorGridLines { get; set; } = new GridLineOptions { Color = Color.FromRgb(255, 0, 0), Opacity = 1, Width = 4 };
    public GridLineOptions MinorGridLines { get; set; } = new GridLineOptions { Color = Color.FromRgb(255, 0, 0), Opacity = 1, Width = 1 };
    public DebugGridOrigin GridOrigin { get; set; }

    //public bool Inverse { get; set; }
    //public bool MakeGridRainbows { get; set; }

    public DebugGrid()
    {
        // The debug grid should always just be a visual helper.
        InputTransparent = true;
    }
}
